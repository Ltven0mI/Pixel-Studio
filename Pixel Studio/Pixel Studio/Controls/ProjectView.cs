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
    public partial class ProjectView : UserControl
    {
        [Browsable(false)]
        public ProjectHandler ProjectHandler { get; set; }
        private List<Project> Projects { get { return ProjectHandler?.Projects; } }
        public Project ActiveProject { get { return ProjectHandler?.ActiveProject; } }
        public Project ActiveCloseProject { get; private set; }
        public Project FocusedProject { get; private set; }
        public Project FocusedCloseProject { get; private set; }


        public int TabWidth { get; set; } = 110;
        public int BottomLine { get; set; } = 3;


        private bool LeftDown;


        public ProjectView()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (ProjectHandler != null)
            {
                //e.Graphics.SmoothingMode = SmoothingMode.None;
                //e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

                for (int i = 0; i < ProjectHandler.Projects.Count; i++)
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

                    int closeBtnSize = Size.Height - BottomLine - 6;

                    project.TabRectangle = new Rectangle(i * TabWidth, 0, TabWidth, Size.Height - BottomLine);
                    project.TabCloseRectangle = new Rectangle(project.TabRectangle.X + project.TabRectangle.Width - closeBtnSize - 3, 3, closeBtnSize, closeBtnSize);
                    e.Graphics.FillRectangle(new SolidBrush(tabColor), project.TabRectangle);

                    if (project == FocusedCloseProject || project == ActiveCloseProject)
                        e.Graphics.FillRectangle(new SolidBrush(closeColor), project.TabCloseRectangle);

                    if (project == ActiveProject || project == FocusedProject)
                        e.Graphics.DrawImage(Properties.Resources.tab_close_active, project.TabCloseRectangle);

                    e.Graphics.DrawString("project", DefaultFont, new SolidBrush(textColor), project.TabRectangle.X, project.TabRectangle.Y);
                }
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
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (ActiveProject != null)
            {
                int index = e.X / TabWidth;
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
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                LeftDown = false;
                if (FocusedCloseProject != null && ActiveCloseProject != null && FocusedCloseProject == ActiveCloseProject)
                    ProjectHandler.RemoveProject(ActiveCloseProject);
                FocusedCloseProject = null;
                ActiveCloseProject = null;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            FocusedProject = null;
            Invalidate();
        }
    }
}
