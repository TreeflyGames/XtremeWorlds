using DarkUI.Config;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms; // Required for MessageBox, DialogResult

namespace DarkUI.Docking
{
    /// <summary>
    /// Represents a dockable document window within the DarkUI framework.
    /// Provides base functionality for file handling, dirty state tracking, and persistence.
    /// </summary>
    [ToolboxItem(false)]
    public class DarkDocument : DarkDockContent
    {
        #region Events

        /// <summary>
        /// Fired when the dirty state of the document changes.
        /// </summary>
        public event EventHandler? DirtyChanged;

        /// <summary>
        /// Fired when the associated file path of the document changes.
        /// </summary>
        public event EventHandler? FilePathChanged;

        /// <summary>
        /// Fired when the read-only state of the document changes.
        /// </summary>
        public event EventHandler? ReadOnlyChanged;

        /// <summary>
        /// Fired just before the document attempts to save. Can be cancelled.
        /// </summary>
        public event EventHandler<CancelEventArgs>? Saving;

        /// <summary>
        /// Fired after the document has been successfully saved.
        /// </summary>
        public event EventHandler? Saved;

        /// <summary>
        /// Fired after the document content has been loaded (or failed to load).
        /// Includes success status and potential error message.
        /// </summary>
        public event EventHandler<DocumentLoadEventArgs>? Loaded;

        /// <summary>
        /// Fired when the document is closing, allowing for cancellation (e.g., if save prompt is cancelled).
        /// Note: This is different from the base FormClosing event.
        /// </summary>
        public event EventHandler<CancelEventArgs>? DocumentClosing;


        #endregion

        #region Fields

        private bool _isDirty = false;
        private bool _isReadOnly = false;
        private bool _isNew = true; // A new document is initially considered "new" until first save
        private string? _filePath = null;
        private string? _originalFilePath = null; // Store path at load/creation for Save As comparison
        private string _defaultUntitledText = "Untitled"; // Customizable default name
        private string _dirtyIndicator = "*"; // Character to show when dirty

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default text used for new, unsaved documents.
        /// </summary>
        [Category("Behavior")]
        [Description("The default text used for the tab of new, unsaved documents.")]
        [DefaultValue("Untitled")]
        public string DefaultUntitledText
        {
            get => _defaultUntitledText;
            set
            {
                if (_defaultUntitledText != value)
                {
                    _defaultUntitledText = value;
                    if (IsNew) UpdateTabText(); // Update immediately if relevant
                }
            }
        }

        /// <summary>
        /// Gets or sets the indicator appended to the tab text when the document is dirty.
        /// </summary>
        [Category("Appearance")]
        [Description("The indicator appended to the tab text when the document is dirty.")]
        [DefaultValue("*")]
        public string DirtyIndicator
        {
            get => _dirtyIndicator;
            set
            {
                if (_dirtyIndicator != value)
                {
                    _dirtyIndicator = value;
                    UpdateTabText(); // Update immediately if relevant
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the document content has been modified since the last save.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDirty
        {
            get => _isDirty;
            protected set // Allow derived classes to set dirty flag directly
            {
                if (_isDirty != value)
                {
                    _isDirty = value;
                    UpdateTabText(); // Update UI
                    OnDirtyChanged(EventArgs.Empty); // Fire event
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the document is read-only.
        /// Derived classes should implement logic to prevent editing if this is true.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsReadOnly
        {
            get => _isReadOnly;
            protected set // Allow derived classes to set read-only state
            {
                if (_isReadOnly != value)
                {
                    _isReadOnly = value;
                    OnReadOnlyChanged(EventArgs.Empty); // Fire event
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the document represents a new file that has not yet been saved.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNew
        {
            get => _isNew;
            private set // Only changed internally, typically on save/load
            {
                _isNew = value;
                UpdateTabText(); // Update potentially "Untitled" name
            }
        }


        /// <summary>
        /// Gets or sets the full file path associated with this document.
        /// Setting this path does not automatically load the file; use LoadFromFile explicitly.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string? FilePath
        {
            get => _filePath;
            protected set // Change triggered by Load/Save usually
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    // Update IsNew flag based on whether a path is set
                    // (A new document becomes non-new once it has a path after saving)
                    // IsNew is more reliably set *during* Save/Load methods.

                    UpdateTabText(); // Update UI
                    UpdateToolTipText();
                    OnFilePathChanged(EventArgs.Empty); // Fire event
                }
            }
        }

        /// <summary>
        /// Hides the base property. Documents default to the Document area.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DarkDockArea DefaultDockArea
        {
            get { return base.DefaultDockArea; }
            // No setter - forced in constructor
        }

        #endregion

        #region Constructor Region

        /// <summary>
        /// Initializes a new instance of the <see cref="DarkDocument"/> class as a new, untitled document.
        /// </summary>
        public DarkDocument()
            : this(null, null) // Call the main constructor with null path
        {
            // Specific setup for a new document
            IsNew = true;
            IsDirty = false; // A new, empty document isn't initially dirty
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DarkDocument"/> class and attempts to load content from the specified file path.
        /// </summary>
        /// <param name="filePath">The full path to the file to load.</param>
        public DarkDocument(string filePath)
            : this(filePath, null)
        {
        }

        /// <summary>
        /// Internal constructor used for initialization and persistence.
        /// </summary>
        /// <param name="filePath">Optional file path to load.</param>
        /// <param name="persistString">Optional persistence string (used during layout loading).</param>
        internal DarkDocument(string? filePath, string? persistString)
        {
            BackColor = Colors.GreyBackground;
            base.DefaultDockArea = DarkDockArea.Document;
            DockText = DefaultUntitledText; // Start with default text
            UpdateToolTipText();

            // Handle loading if a path is provided directly or via persistence string
            string? loadPath = filePath;
            if (!string.IsNullOrEmpty(persistString))
            {
                // Expected format: "{BasePersistString}::{FilePath}" or just "{BasePersistString}"
                string[] parts = persistString.Split(new[] { "::" }, StringSplitOptions.None);
                if (parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1]))
                {
                    loadPath = parts[1]; // Path from persistence overrides direct path
                }
            }

            if (!string.IsNullOrWhiteSpace(loadPath))
            {
                // Use BeginInvoke to ensure Handle is created before loading content
                if (IsHandleCreated)
                {
                    BeginInvoke(new Action(() => LoadFromFile(loadPath)));
                }
                else
                {
                    // If handle isn't created yet, postpone load until it is
                    // HandleCreated event might be too early, Load event is safer
                    Load += (sender, e) =>
                    {
                        // Ensure it only runs once if Load fires multiple times
                        if (IsNew && FilePath == null)
                        {
                           BeginInvoke(new Action(() => LoadFromFile(loadPath)));
                        }
                    };
                }
            }
            else
            {
                 IsNew = true;
                 IsDirty = false;
                 UpdateTabText(); // Set initial "Untitled" text
            }
        }


        #endregion

        #region Virtual Methods for Overriding

        /// <summary>
        /// Loads the document content from the specified file path.
        /// Derived classes MUST override this to load their specific content.
        /// Base implementation sets the FilePath and IsNew/IsDirty flags.
        /// </summary>
        /// <param name="filePath">The full path to the file to load.</param>
        /// <returns>True if loading was successful, false otherwise.</returns>
        public virtual bool LoadFromFile(string filePath)
        {
            bool success = false;
            string errorMessage = string.Empty;
            try
            {
                // --- Derived class load logic goes here ---
                // Example:
                // if (!File.Exists(filePath)) throw new FileNotFoundException("File not found.", filePath);
                // // Read file content into internal representation (e.g., text box, image object)
                // // myTextBox.Text = File.ReadAllText(filePath);
                // success = true; // Set to true if derived class load succeeds

                // Simulate success for base class example
                 Console.WriteLine($"Base LoadFromFile called for: {filePath}. Derived class should implement loading.");
                 success = true;
                // -----------------------------------------

                if (success)
                {
                    FilePath = filePath;
                    _originalFilePath = filePath; // Store original path
                    IsNew = false;
                    IsDirty = false; // Content matches file after load
                    IsReadOnly = (File.GetAttributes(filePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
                }
            }
            catch (Exception ex)
            {
                success = false;
                errorMessage = ex.Message;
                Console.WriteLine($"Error loading document '{filePath}': {ex.Message}");
                // Optionally show an error message to the user here or rely on the event
            }
            finally
            {
                // Fire event regardless of success
                OnLoaded(new DocumentLoadEventArgs(success, errorMessage, filePath));
            }
            return success;
        }

        /// <summary>
        /// Saves the document content. If FilePath is null or empty, calls SaveAs().
        /// Derived classes SHOULD override this to save their specific content.
        /// Base implementation handles dirty flag, events, and path management.
        /// </summary>
        /// <returns>True if saving was successful or not needed, false otherwise (e.g., cancelled, error).</returns>
        public virtual bool Save()
        {
            if (string.IsNullOrWhiteSpace(FilePath))
            {
                return SaveAs(); // If no path, force Save As dialog
            }
            else
            {
                return SaveToFile(FilePath); // Save to existing path
            }
        }

        /// <summary>
        /// Prompts the user for a file path using SaveFileDialog and saves the document content.
        /// Derived classes MAY override this, but usually overriding SaveToFile is sufficient.
        /// </summary>
        /// <returns>True if saving was successful, false otherwise (e.g., cancelled, error).</returns>
        public virtual bool SaveAs()
        {
            using (var sfd = new SaveFileDialog())
            {
                // Configure dialog (filters should be set based on document type)
                sfd.Filter = "All files (*.*)|*.*"; // Example filter - override this!
                sfd.FileName = Path.GetFileName(FilePath ?? DefaultUntitledText);
                if (!string.IsNullOrEmpty(FilePath))
                {
                    sfd.InitialDirectory = Path.GetDirectoryName(FilePath);
                }

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    return SaveToFile(sfd.FileName); // Save to the new path
                }
                else
                {
                    return false; // User cancelled Save As dialog
                }
            }
        }

        /// <summary>
        /// Saves the document content to the specified file path.
        /// Derived classes MUST override this to save their specific content.
        /// Base implementation handles flags, events, and basic error structure.
        /// </summary>
        /// <param name="filePath">The full path to save the file to.</param>
        /// <returns>True if saving was successful, false otherwise.</returns>
        protected virtual bool SaveToFile(string filePath)
        {
            var args = new CancelEventArgs();
            OnSaving(args);
            if (args.Cancel) return false; // Saving cancelled by event handler

            bool success = false;
            try
            {
                Console.WriteLine($"Base SaveToFile called for: {filePath}. Derived class should implement saving.");
                // --- Derived class save logic goes here ---
                // Example:
                // File.WriteAllText(filePath, myTextBox.Text);
                // success = true; // Set true if derived class save succeeds
                success = true; // Simulate success
                // -----------------------------------------

                if (success)
                {
                    FilePath = filePath; // Update path if changed (e.g., from Save As)
                    if (IsNew) _originalFilePath = filePath; // Store original path on first successful save
                    IsNew = false;
                    IsDirty = false; // Saved content now matches file
                    OnSaved(EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine($"Error saving document '{filePath}': {ex.Message}");
                // Show error message to user
                MessageBox.Show($"Failed to save file '{Path.GetFileName(filePath)}'.\n\nError: {ex.Message}",
                                "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return success;
        }

        /// <summary>
        /// Initiates the closing process for the document. Checks for dirty state and prompts the user to save if necessary.
        /// Can be cancelled by the user or event handlers.
        /// </summary>
        /// <param name="force">If true, closes without prompting, potentially discarding changes.</param>
        /// <returns>True if the document did close or should close, false if the closing process was cancelled.</returns>
        public virtual bool CloseDocument(bool force = false)
        {
            var args = new CancelEventArgs();
            OnDocumentClosing(args);
            if (args.Cancel) return false; // Cancelled by external event handler

            if (!force && IsDirty)
            {
                // Prompt user to save changes
                string promptMessage = $"Save changes to '{this.DockText.TrimEnd(DirtyIndicator[0])}'?"; // Use DockText without dirty marker

                DialogResult result = MessageBox.Show(promptMessage, "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (!Save()) // Attempt to save; if save fails or is cancelled...
                    {
                        return false; // ...cancel the close operation
                    }
                    // Save successful, proceed to close
                }
                else if (result == DialogResult.Cancel)
                {
                    return false; // User cancelled the close operation
                }
                // If result is No, proceed to close without saving
            }

            // If not dirty, not forced, or user chose No, proceed to actual closing
            // The DockPanel should handle the final removal after this returns true
            return true;
        }

        /// <summary>
        /// Called when the document content needs to be printed. Derived classes should override this.
        /// </summary>
        public virtual void Print()
        {
            // Placeholder - Derived classes implement printing logic
             MessageBox.Show("Print functionality not implemented for this document type.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Gets text information to potentially display in a status bar.
        /// </summary>
        /// <returns>Status bar text string, or null/empty.</returns>
        public virtual string? GetStatusBarInfo()
        {
            return FilePath ?? (IsNew ? DefaultUntitledText : string.Empty);
        }

        // --- Standard Edit Command Hooks (for Main Menu/Toolbar enabling/disabling) ---
        public virtual bool CanUndo() => false;
        public virtual void Undo() { }
        public virtual bool CanRedo() => false;
        public virtual void Redo() { }
        public virtual bool CanCut() => false;
        public virtual void Cut() { }
        public virtual bool CanCopy() => false;
        public virtual void Copy() { }
        public virtual bool CanPaste() => false;
        public virtual void Paste() { }
        public virtual bool CanSelectAll() => false;
        public virtual void SelectAll() { }

        #endregion

        #region Event Raising Methods

        protected virtual void OnDirtyChanged(EventArgs e) => DirtyChanged?.Invoke(this, e);
        protected virtual void OnFilePathChanged(EventArgs e) => FilePathChanged?.Invoke(this, e);
        protected virtual void OnReadOnlyChanged(EventArgs e) => ReadOnlyChanged?.Invoke(this, e);
        protected virtual void OnSaving(CancelEventArgs e) => Saving?.Invoke(this, e);
        protected virtual void OnSaved(EventArgs e) => Saved?.Invoke(this, e);
        protected virtual void OnLoaded(DocumentLoadEventArgs e) => Loaded?.Invoke(this, e);
        protected virtual void OnDocumentClosing(CancelEventArgs e) => DocumentClosing?.Invoke(this, e);

        #endregion

        #region Overrides

        /// <summary>
        /// Overrides the base Close method to integrate dirty checking and save prompting.
        /// </summary>
        public override void Close()
        {
            // Attempt to close gracefully using our document logic
            if (CloseDocument(force: false))
            {
                // If CloseDocument allows it (returns true), call the base Close
                // which handles disposal and removal from the DockPanel.
                base.Close();
            }
            // If CloseDocument returns false, the operation was cancelled, so do nothing more.
        }

        /// <summary>
        /// Gets the persistence string for saving the docking layout. Includes the file path.
        /// </summary>
        /// <returns>A string combining type information and the file path.</returns>
        protected string GetPersistString()
        {
            // Include file path for potentially reloading the document when the layout is restored.
            // Format: "{BasePersistString}::{FilePath}"
            // BasePersistString usually contains type information.
            string baseString = GetType().ToString();
            return $"{baseString}::{FilePath ?? string.Empty}";
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Updates the TabText and potentially ToolTipText based on file path and dirty status.
        /// </summary>
        protected virtual void UpdateTabText()
        {
            string baseName;
            if (!string.IsNullOrEmpty(FilePath))
            {
                baseName = Path.GetFileName(FilePath);
            }
            else if (IsNew)
            {
                baseName = DefaultUntitledText;
            }
            else
            {
                baseName = DefaultUntitledText; // Fallback
            }

            string dirtyMark = IsDirty ? DirtyIndicator : string.Empty;

            // Use DockText property from DarkDockContent
            DockText = $"{baseName}{dirtyMark}";

             // Optional: Update ToolTipText as well
             // UpdateToolTipText(); // Call if tooltip should also reflect dirty status/path
        }

        /// <summary>
        /// Updates the ToolTipText, typically showing the full file path.
        /// </summary>
        protected virtual void UpdateToolTipText()
        {
            // Use ToolTipText property from DarkDockContent
            this.DockText = FilePath ?? string.Empty;
        }

        #endregion
    }

    #region Supporting EventArgs

    /// <summary>
    /// Provides data for the Loaded event of a DarkDocument.
    /// </summary>
    public class DocumentLoadEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a value indicating whether the document was loaded successfully.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Gets an optional error message if loading failed.
        /// </summary>
        public string? ErrorMessage { get; }

        /// <summary>
        /// Gets the file path that was attempted to load.
        /// </summary>
        public string? FilePath { get; }

        public DocumentLoadEventArgs(bool success, string? errorMessage, string? filePath)
        {
            Success = success;
            ErrorMessage = errorMessage;
            FilePath = filePath;
        }
    }

    #endregion
}
