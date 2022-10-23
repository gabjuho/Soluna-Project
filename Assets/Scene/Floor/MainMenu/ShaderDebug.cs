using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderDebug : MonoBehaviour
{
    private void Start()
    {
        MeshRenderer[] renderers = FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];

        foreach (MeshRenderer item in renderers)
        {
            if(item.material != null)
            {
                foreach(Material mat in item.materials)
                {
                    Shader sha = mat.shader;
                    sha = Shader.Find(sha.name);
                    mat.shader = sha;

                    item.material = mat;
                }
            }
        }
    }


}
