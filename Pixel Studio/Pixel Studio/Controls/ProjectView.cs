﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pixel_Studio.Components;

namespace Pixel_Studio.Controls
{
    public partial class ProjectView : UserControl
    {
        [Browsable(false)]
        public ProjectHandler ProjectHandler { get; set; }
        private List<Project> Projects { get { return ProjectHandler?.Projects; } }
        public Project ActiveProject { get { return ProjectHandler?.ActiveProject; } }
        public Project FocusedProject { get; private set; }


        public int TabWidth { get; set; } = 110;
        public int BottomLine { get; set; } = 3;


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
                for (int i = 0; i < ProjectHandler.Projects.Count; i++)
                {
                    Project project = ProjectHandler.Projects[i];

                    Color tabColor = ThemeManager.ActiveTheme.BackColor;
                    if (project == ActiveProject)
                        tabColor = ThemeManager.ActiveTheme.TabActiveColor;
                    else if (project == FocusedProject)
                        tabColor = ThemeManager.ActiveTheme.TabFocusedColor;

                    project.TabRectangle = new Rectangle(i * TabWidth, 0, TabWidth, Size.Height - BottomLine);
                    e.Graphics.FillRectangle(new SolidBrush(tabColor), project.TabRectangle);
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
            if (FocusedProject != null)
                ProjectHandler.SetActiveProject(FocusedProject);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int index = e.X / TabWidth;
            if (index >= 0 && index < Projects.Count && (FocusedProject != null && index != FocusedProject.Index || FocusedProject == null))
            {
                FocusedProject = Projects[index];
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
