using UnityEngine;

public class OptimizedShapeRenderer : MonoBehaviour
{
    public enum Shape
     { 
        Cube, 
        Pyramid, 
        Sphere 
    }
    
    public Shape shapeToRender = Shape.Cube;
    public Material mat;

    void OnRenderObject()
    {
        if (mat == null) return;

        mat.SetPass(0);
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);


        switch (shapeToRender)
        {
            case Shape.Cube:
                DrawCube();
                break;
            case Shape.Pyramid:
                DrawPyramid();
                break;
            case Shape.Sphere:
                DrawSphere();
                break;
        }

        GL.PopMatrix();
    }

    void DrawCube()
    {
        GL.Begin(GL.QUADS);
        
        GL.Vertex3(-0.5f, -0.5f, -0.5f); GL.Vertex3(-0.5f,  0.5f, -0.5f); GL.Vertex3( 0.5f,  0.5f, -0.5f); GL.Vertex3( 0.5f, -0.5f, -0.5f);
        GL.Vertex3( 0.5f, -0.5f,  0.5f); GL.Vertex3( 0.5f,  0.5f,  0.5f); GL.Vertex3(-0.5f,  0.5f,  0.5f); GL.Vertex3(-0.5f, -0.5f,  0.5f);
        GL.Vertex3(-0.5f,  0.5f, -0.5f); GL.Vertex3(-0.5f,  0.5f,  0.5f); GL.Vertex3( 0.5f,  0.5f,  0.5f); GL.Vertex3( 0.5f,  0.5f, -0.5f);
        GL.Vertex3(-0.5f, -0.5f,  0.5f); GL.Vertex3(-0.5f, -0.5f, -0.5f); GL.Vertex3( 0.5f, -0.5f, -0.5f); GL.Vertex3( 0.5f, -0.5f,  0.5f);
        GL.Vertex3(-0.5f, -0.5f,  0.5f); GL.Vertex3(-0.5f,  0.5f,  0.5f); GL.Vertex3(-0.5f,  0.5f, -0.5f); GL.Vertex3(-0.5f, -0.5f, -0.5f);
        GL.Vertex3( 0.5f, -0.5f, -0.5f); GL.Vertex3( 0.5f,  0.5f, -0.5f); GL.Vertex3( 0.5f,  0.5f,  0.5f); GL.Vertex3( 0.5f, -0.5f,  0.5f);

        GL.End();
    }

    void DrawPyramid()
    {

        GL.Begin(GL.TRIANGLES);
        GL.Vertex(new Vector3(-0.5f, -0.5f, -0.5f)); GL.Vertex(new Vector3(0, 0.5f, 0)); GL.Vertex(new Vector3(0.5f, -0.5f, -0.5f));
        GL.Vertex(new Vector3(0.5f, -0.5f, -0.5f)); GL.Vertex(new Vector3(0, 0.5f, 0)); GL.Vertex(new Vector3(0.5f, -0.5f,  0.5f));
        GL.Vertex(new Vector3(0.5f, -0.5f,  0.5f)); GL.Vertex(new Vector3(0, 0.5f, 0)); GL.Vertex(new Vector3(-0.5f, -0.5f,  0.5f));
        GL.Vertex(new Vector3(-0.5f, -0.5f,  0.5f)); GL.Vertex(new Vector3(0, 0.5f, 0)); GL.Vertex(new Vector3(-0.5f, -0.5f, -0.5f));
        GL.End();

        GL.Begin(GL.QUADS);
        GL.Vertex(new Vector3(-0.5f, -0.5f,  0.5f)); GL.Vertex(new Vector3(0.5f, -0.5f,  0.5f)); GL.Vertex(new Vector3(0.5f, -0.5f, -0.5f)); GL.Vertex(new Vector3(-0.5f, -0.5f, -0.5f));
        GL.End();
    }

    void DrawSphere()
    {
        int totalRings = 16;
        int totalSlices = 16;

        for (int r = 0; r < totalRings; r++)
        {
            GL.Begin(GL.TRIANGLE_STRIP);

            float currentRingPct = (float)r / totalRings;
            float nextRingPct = (float)(r + 1) / totalRings;

            float latAngle1 = currentRingPct * Mathf.PI;
            float latAngle2 = nextRingPct * Mathf.PI;

            for (int s = 0; s <= totalSlices; s++)
            {
                float slicePct = (float)s / totalSlices;
                
                float lonAngle = slicePct * (2f * Mathf.PI);

                GL.Vertex(CirclePoint(latAngle1, lonAngle));
                GL.Vertex(CirclePoint(latAngle2, lonAngle));
            }

            GL.End();
        }
    }

    Vector3 CirclePoint(float lat, float lon)
    {
        float x = Mathf.Sin(lat) * Mathf.Cos(lon);
        float y = Mathf.Cos(lat);
        float z = Mathf.Sin(lat) * Mathf.Sin(lon);

        return new Vector3(x, y, z) * 1;
    }
}