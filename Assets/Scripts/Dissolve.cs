using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public MeshRenderer generatorMesh;
    private Material[] generatorMaterials;
    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;
    void Start()
    {
        if (generatorMesh != null){
            generatorMaterials = generatorMesh.materials;
        }
        StartCoroutine(DissolveCo());
    }

    public IEnumerator DissolveCo(){
        yield return new WaitForSeconds(0.5f);
        if(generatorMaterials.Length > 0){
            float counter = 0;
            while(generatorMaterials[0].GetFloat("_DissolveAmount") < 1){
                counter += dissolveRate;
                for(int i = 0; i < generatorMaterials.Length; i++){
                    generatorMaterials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }

}
