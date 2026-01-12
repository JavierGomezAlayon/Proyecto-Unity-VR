using UnityEngine;

public class InvertirEsfera : MonoBehaviour
{
    void Start()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        if (filter != null)
        {
            Mesh mesh = filter.mesh;

            // 1. Invertir las normales (para que la luz rebote bien desde dentro)
            Vector3[] normals = mesh.normals;
            for (int i = 0; i < normals.Length; i++)
                normals[i] = -normals[i];
            mesh.normals = normals;

            // 2. Invertir los triángulos (para que Unity sepa que la cara visible es la de dentro)
            for (int m = 0; m < mesh.subMeshCount; m++)
            {
                int[] triangles = mesh.GetTriangles(m);
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    int temp = triangles[i + 0];
                    triangles[i + 0] = triangles[i + 1];
                    triangles[i + 1] = temp;
                }
                mesh.SetTriangles(triangles, m);
            }
        }
    }
}
