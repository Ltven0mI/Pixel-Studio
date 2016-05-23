using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pixel_Studio.Components;
using System.Drawing.Drawing2D;

namespace Pixel_Studio.Controls
{
    public partial class ProjectView : ProjectControl
    {
        public Project ActiveCloseProject { get; private set; }
        public Project FocusedProject { get; private set; }
        public Project FocusedCloseProject { get; private set; }


        public int TabWidth { get; set; } = 110;
        public int BottomLine { get; set; } = 3;

        public StringFormat StringFormat;


        private bool LeftDown;

        private int VisibleProjectCount;

        private Rectangle ProjectButtonRect;
        private bool ProjectButtonFocused;
        private ContextMenuStrip ProjectContextMenu;


        public ProjectView()
        {
            InitializeComponent();
            DoubleBuffered = true;

            StringFormat = new StringFormat();
            StringFormat.Alignment = StringAlignment.Near;
            StringFormat.LineAlignment = StringAlignment.Center;
            StringFormat.Trimming = StringTrimming.None;

            ProjectContextMenu = new ContextMenuStrip();
            ProjectContextMenu.ItemClicked += ProjectContextMenu_ItemClicked;
            ProjectContextMenu.VisibleChanged += ProjectContextMenu_VisibleChanged;

            UpdateProjectButtonRect();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (ProjectHandler != null)
            {
                //e.Graphics.SmoothingMode = SmoothingMode.None;
                //e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                for (int i = 0; i < Math.Min(ProjectHandler.Projects.Count, VisibleProjectCount); i++)
                {
                    Project project = ProjectHandler.Projects[i];

                    Color tabColor = ThemeManager.ActiveTheme.BackColor;
                    Color textColor = ThemeManager.ActiveTheme.ForeColor;
                    Color closeColor = ThemeManager.ActiveTheme.BackColor;
                    if (project == ActiveProject)
                    {
                        tabColor = ThemeManager.ActiveTheme.TabActiveColor;
                        if (project == ActiveCloseProject && FocusedCloseProject != null)
                            closeColor = ThemeManager.ActiveTheme.TabFocusedCloseActiveColor;
                        else
                            closeColor = ThemeManager.ActiveTheme.TabFocusedCloseFocusedColor;
                    }
                    else if (project == FocusedProject)
                    {
                        tabColor = ThemeManager.ActiveTheme.TabFocusedColor;
                        if (project == ActiveCloseProject && project == FocusedCloseProject)
                            closeColor = ThemeManager.ActiveTheme.TabActiveCloseActiveColor;
                        else
                            closeColor = ThemeManager.ActiveTheme.TabFocusedCloseFocusedColor;
                    }
                    
                    if (LeftDown && project == ActiveProject && project == FocusedProject && ActiveCloseProject == null)
                    {
                        int closeBtnSize = Size.Height - BottomLine - 6;

                        project.TabRectangle = new Rectangle(i * TabWidth, BottomLine, TabWidth, Size.Height - BottomLine * 2);
                        project.TabCloseRectangle = new Rectangle(project.TabRectangle.X + project.TabRectangle.Width - closeBtnSize - 3, 3 + BottomLine, closeBtnSize, closeBtnSize);
                        e.Graphics.FillRectangle(new SolidBrush(tabColor), project.TabRectangle);
                    }
                    else
                    {
                        int closeBtnSize = Size.Height - BottomLine - 6;

                        project.TabRectangle = new Rectangle(i * TabWidth, 0, TabWidth, Size.Height - BottomLine);
                        project.TabCloseRectangle = new Rectangle(project.TabRectangle.X + project.TabRectangle.Width - closeBtnSize - 3, 3, closeBtnSize, closeBtnSize);
                        e.Graphics.FillRectangle(new SolidBrush(tabColor), project.TabRectangle);
                    }

                    if (project == FocusedCloseProject || project == ActiveCloseProject)
                        e.Graphics.FillRectangle(new SolidBrush(closeColor), project.TabCloseRectangle);

                    if (project == ActiveProject || project == FocusedProject)
                        e.Graphics.DrawImage(Properties.Resources.tab_close_active, project.TabCloseRectangle);

                    Rectangle strinRect = new Rectangle(project.TabRectangle.X, project.TabRectangle.Y, project.TabRectangle.Width - project.TabCloseRectangle.Width - 6, project.TabRectangle.Height);
                    e.Graphics.DrawString(project.Name, DefaultFont, new SolidBrush(textColor), strinRect, StringFormat);
                }

                if (ProjectButtonFocused || ProjectContextMenu.Visible)
                {
                    Color projectButtonColor = ThemeManager.ActiveTheme.ProjectButtonFocusedColor;
                    
                    if (ProjectContextMenu.Visible)
                        projectButtonColor = ThemeManager.ActiveTheme.ProjectButtonActiveColor;

                    UpdateProjectButtonRect();
                    e.Graphics.FillRectangle(new SolidBrush(projectButtonColor), ProjectButtonRect);
                }

                e.Graphics.DrawImage(Properties.Resources.projectlist_active, ProjectButtonRect);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            e.Graphics.FillRectangle(new SolidBrush(ThemeManager.ActiveTheme.BackColor), e.ClipRectangle);
            e.Graphics.FillRectangle(new SolidBrush(ThemeManager.ActiveTheme.TabActiveColor), 0, Size.Height - BottomLine, Size.Width, BottomLine);
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                LeftDown = true;

                if (FocusedCloseProject != null)
                    ActiveCloseProject = FocusedCloseProject;
                else if (FocusedProject != null)
                    ProjectHandler.SetActiveProject(FocusedProject);

                if (ProjectButtonRect.Contains(e.Location))
                    ShowProjectContextMenu();
                else
                    ProjectButtonFocused = false;

                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (ActiveProject != null)
            {
                int index = e.X / TabWidth;
                if (index < VisibleProjectCount)
                {
                    if (LeftDown)
                    {
                        if (index != ActiveProject.Index && ActiveCloseProject == null)
                            ProjectHandler.MoveProject(ActiveProject, index);
                    }

                    if (index >= 0 && index < Projects.Count)
                    {
                        if (!LeftDown && (FocusedProject != null && index != FocusedProject.Index || FocusedProject == null))
                            FocusedProject = Projects[index];
                        if ((!LeftDown || ActiveCloseProject != null && ActiveCloseProject == FocusedProject) && FocusedProject.TabCloseRectangle.Contains(e.X, e.Y))
                            FocusedCloseProject = FocusedProject;
                        else
                            FocusedCloseProject = null;
                        Invalidate();
                    }
                }
                else if (!LeftDown)
                {
                    FocusedProject = null;
                    FocusedCloseProject = null;
                    Invalidate();
                }
            }

            if (ProjectButtonRect.Contains(e.Location))
            {
                    ProjectButtonFocused = true;
                    Invalidate();
            }
            else if (ProjectButtonFocused)
            {
                ProjectButtonFocused = false;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                LeftDown = false;
                if (FocusedCloseProject != null && ActiveCloseProject != null && FocusedCloseProject == ActiveCloseProject)
                    ProjectHandler.RemoveProject(ActiveCloseProject);
                FocusedProject = null;
                FocusedCloseProject = null;
                ActiveCloseProject = null;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            FocusedProject = null;
            ProjectButtonFocused = false;
            Invalidate();
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateVisibleProjectCount();
            UpdateProjectButtonRect();
            Invalidate();
        }


        protected override void OnProjectAdded(object sender, ProjectHandler.ProjectEventArgs e)
        {
            base.OnProjectAdded(sender, e);
            UpdateVisibleProjectCount();
            if (!Visible) Visible = true;
        }

        protected override void OnProjectRemoved(object sender, ProjectHandler.ProjectEventArgs e)
        {
            base.OnProjectRemoved(sender, e);
            UpdateVisibleProjectCount();
            if (Visible && Projects.Count == 0) Visible = false;
        }


        // Methods ///////////////////////////////////////////////////////////////////////////////////////////////
        private void ShowProjectContextMenu()
        {
            ProjectContextMenu.Items.Clear();

            foreach (Project project in Projects)
                ProjectContextMenu.Items.Add(project.Name);

            ProjectContextMenu.Show(this, new Point(ProjectButtonRect.Right, ProjectButtonRect.Bottom), ToolStripDropDownDirection.BelowLeft);
        }


        // Update Variables ///////////////////////////////////////////////////////////////////////////////////////
        private void UpdateVisibleProjectCount()
        {
            VisibleProjectCount = (Size.Width - ProjectButtonRect.Width) / TabWidth;
        }

        private void UpdateProjectButtonRect()
        {
            ProjectButtonRect.Width = Size.Height - BottomLine - 5;
            ProjectButtonRect.Height = ProjectButtonRect.Width;
            ProjectButtonRect.X = Size.Width - ProjectButtonRect.Width;
            ProjectButtonRect.Y = 2;
        }


        // Event Handlers Methods //

        // Project Context Menu
        private void ProjectContextMenu_VisibleChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ProjectContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ProjectHandler.SetActiveProject(ProjectContextMenu.Items.IndexOf(e.ClickedItem));
        }
    }
}
