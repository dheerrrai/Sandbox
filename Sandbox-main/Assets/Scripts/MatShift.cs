using UnityEngine;

public class MatShift : MonoBehaviour
{
    
    public MeshRenderer CapsuleMaterial;
    public MeshRenderer CylinderMaterial;
    public Material[] MaterialList;
    public void MatChange(int id)
    {
        CapsuleMaterial.material = MaterialList[id];
        CylinderMaterial.material = MaterialList[id];
    }

    public void SetTransform()
    {
        transform.position = new Vector3(0,0,0);
    }
}
