using System.ComponentModel;
using Assimp;

namespace scene_raycasting_3D.FormsWidgets;

internal class ViewPictureBox: PictureBox
{
    public ViewPictureBox(IContainer container, View view)
    {
        _view = view;
    }

    private Point? _lastMousePoint;
    private readonly View _view;

    protected override void OnMouseDown(MouseEventArgs e)
    {
        _lastMousePoint = e.Location;
        base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (!_lastMousePoint.HasValue) return;
        if (e.Button == MouseButtons.Left)
        {
            _view.Move(new Vector2D(e.X - _lastMousePoint.Value.X, e.Y - _lastMousePoint.Value.Y));
            _lastMousePoint = e.Location;
            _view.Refresh();
            Refresh();
        }
        if (e.Button == MouseButtons.Right)
        {
            _view.MoveTheSun(new Vector2D(e.X - _lastMousePoint.Value.X, e.Y - _lastMousePoint.Value.Y));
            _lastMousePoint = e.Location;
            _view.Refresh();
            Refresh();
        }
        base.OnMouseMove(e);
    }
}