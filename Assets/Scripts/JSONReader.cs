using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Results
{
    public int total_clicks;
    public float total_time;
}
public class JSONReader : MonoBehaviour
{
    [SerializeField] TextAsset txtJSON;
    [SerializeField] public BlockList myBlocklist = new BlockList();

    [Serializable]
    public class Block
    {
        public string R;
        public string C;
        public string number;
    }

    [Serializable]
    public class BlockList
    {
        public Block[] blocks;
    }

    // Start is called before the first frame update
    void Awake()
    {
        myBlocklist = JsonUtility.FromJson<BlockList>(txtJSON.text);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
