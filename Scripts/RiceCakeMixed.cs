using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RiceCakeMixed : MonoBehaviour
{
    public bool KinakoTaste { get => kinakoTaste; set => kinakoTaste = value; }
    public bool SoySourceTaste { get => soySourceTaste; set => soySourceTaste = value; }
    private RiceCakeManager riceCakeManager;

    [SerializeField] private StageManager stageManager;

    [SerializeField] private Material kinakoMaterial;
    [SerializeField] private Material soySourceMaterial;

    [SerializeField] private GameObject kinakoParticle;
    [SerializeField] private GameObject soySourceParticle;

    private bool kinakoTaste; //黄な粉味になっているか
    private bool soySourceTaste; //醤油味になっているか
    private  class Tags
    {
        public const string Kinako = "Kinako";
        public const string SoySource = "SoySource";
        public const string Knife = "Knife";
        public const string KinakoRiceCake = "KinakoRiceCake";
        public const string SoySourceRiceCake = "SoySourceRiceCake";
    }

    // Start is called before the first frame update
    void Start()
    {
        //いちいちセットするのは面倒くさいため
        riceCakeManager = FindObjectOfType<RiceCakeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 味を変える処理
    /// </summary>
    /// <param name="tag">tag名</param>
    /// <param name="material">変更する色</param>
    /// <param name="particle">生成するパーティクル</param>
    /// <param name="newTag">変更後のTag</param>
    /// <param name="tasteFlag"></param>
    private void ChangeTaste(string tag, Material material, GameObject particle, string newTag, ref bool tasteFlag)
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        tasteFlag = true;

        if (tag == "Kinako" && !soySourceTaste || tag == "SoySource" && !kinakoTaste)
        {
            Rigidbody riceCakeRb = GetComponent<Rigidbody>();
            if (riceCakeRb != null)
            {
                riceCakeRb.velocity = Vector3.zero;
                riceCakeRb.angularVelocity = Vector3.zero;
            }

            gameObject.tag = newTag;

            Renderer riceCakeRenderer = GetComponent<Renderer>();
            if (riceCakeRenderer != null && material != null)
            {
                riceCakeRenderer.material = material;
            }

            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RiceCake"))//餅同士はくっつく
        {
            GameObject oneMoreRiceCake = collision.gameObject;
            riceCakeManager.OnRiceCakeCollision(gameObject, oneMoreRiceCake);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Kinako))
        {
            ChangeTaste(Tags.Kinako, kinakoMaterial, kinakoParticle, Tags.KinakoRiceCake, ref kinakoTaste);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag(Tags.SoySource))
        {
            ChangeTaste(Tags.SoySource, soySourceMaterial, soySourceParticle, Tags.SoySourceRiceCake, ref soySourceTaste);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Knife"))
        {
            riceCakeManager.CutRiceCake(this.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
