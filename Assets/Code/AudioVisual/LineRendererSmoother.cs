using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererSmoother : MonoBehaviour
{
    public LineRenderer Line;
    public Vector3[] InitialState;
    public float SmoothingLength = 2f;
    public int SmoothingSections = 10;

    private BezierCurve[] Curves;

    public void SmoothPath()
    {
        InitialState = new Vector3[Line.positionCount];
        for(int i = 0; i < InitialState.Length; i++)
        {
            InitialState[i] = Line.GetPosition(i);
        }
        CalculateCurvePoints();
        Line.positionCount = Curves.Length * SmoothingSections;
        int index = 0;
        for (int i = 0; i < Curves.Length; i++)
        {
            Vector3[] segments = Curves[i].GetSegments(SmoothingSections);
            for (int j = 0; j < segments.Length; j++)
            {
                Line.SetPosition(index, segments[j]);
                index++;
            }
        }

        // Reset values so inspector doesn't freeze if you use lots of smoothing sections
        //SmoothingSections = 1;
        //SmoothingLength = 0;
    }

    private void CalculateCurvePoints()
    {
        if (Line.positionCount < 3)
        {
            return;
        }
        EnsureCurvesMatchLineRendererPositions();

        for (int i = 0; i < Curves.Length; i++)
        {
            Vector3 position = Line.GetPosition(i);
            Vector3 lastPosition = i == 0 ? Line.GetPosition(0) : Line.GetPosition(i - 1);
            Vector3 nextPosition = Line.GetPosition(i + 1);

            Vector3 lastDirection = (position - lastPosition).normalized;
            Vector3 nextDirection = (nextPosition - position).normalized;

            Vector3 startTangent = (lastDirection + nextDirection) * SmoothingLength;
            Vector3 endTangent = (nextDirection + lastDirection) * -1 * SmoothingLength;

            Curves[i].Points[0] = position; // Start Position (P0)
            Curves[i].Points[1] = position + startTangent; // Start Tangent (P1)
            Curves[i].Points[2] = nextPosition + endTangent; // End Tangent (P2)
            Curves[i].Points[3] = nextPosition; // End Position (P3)
        }

        // Apply look-ahead for first curve and retroactively apply the end tangent
        {
            Vector3 nextDirection = (Curves[1].EndPosition - Curves[1].StartPosition).normalized;
            Vector3 lastDirection = (Curves[0].EndPosition - Curves[0].StartPosition).normalized;

            Curves[0].Points[2] = Curves[0].Points[3] +
                (nextDirection + lastDirection) * -1 * SmoothingLength;
        }
    }

    public void EnsureCurvesMatchLineRendererPositions()
    {
        if (Curves == null || Curves.Length != Line.positionCount - 1)
        {
            Curves = new BezierCurve[Line.positionCount - 1];
            for (int i = 0; i < Curves.Length; i++)
            {
                Curves[i] = new BezierCurve();
            }
        }
    }
}

