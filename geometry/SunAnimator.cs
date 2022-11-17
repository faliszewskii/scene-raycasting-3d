using System.Security.Policy;
using Assimp;

namespace scene_raycasting_3D.geometry;

public class SunAnimator
{
    public Vector2D TheMiddle { get; set; }
    private double _phase;
    private double _dPhase;
    private readonly double _maxRadius;
    private double _radius;

    public SunAnimator(Vector2D startingPosition, double dPhase, double maxRadius)
    {
        TheMiddle = startingPosition;
        _phase = 0;
        _dPhase = dPhase;
        _radius = 0;
        _maxRadius = maxRadius;
    }

    public Vector2D nextPosition()
    {
        if (_phase + _dPhase > 6 * Math.PI || _phase + _dPhase < 0) _dPhase *= -1;
        _phase += _dPhase;
        _radius = _maxRadius *_phase;
        return new Vector2D((float)(TheMiddle.X + _radius*Math.Cos(_phase)), (float)(TheMiddle.Y + _radius*Math.Sin(_phase)));
    }
}