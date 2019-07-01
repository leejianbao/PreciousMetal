using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class PreciousMetal : MonoBehaviour
{
    public string MetalId;//Id
    public string MetalName;//名称
    public string Dimentions;//尺寸
    public string MadeofMaterial;//材质
    public string IndustrialArt;//工艺
    public string MainObject;//主物品
    public string GiftObject;//赠品
    public double Weight;//重量
    public double Price;//价格
    public string QRCodeUrl;//e商二维码链接
    public string Provider;//贵金属厂商
    public string Categary;//主题分类
    public string ModelUrl;//模型资源链接
    public Int64 Version;//资源版本号
    public Int64 OldVersion;

    public bool AllowRotate;
    private Vector3 Axis;
    private float RotateSpeed;


    // Start is called before the first frame update
    void Start()
    {
        SetInitPosition();
        SetRotatePar();
    }

    private void SetInitPosition()
    {
        initPos = transform.position;
        initAgl = transform.eulerAngles;
        initScale = transform.localScale;
    }
    private void SetRotatePar()
    {
        Axis = Vector3.up;
        RotateSpeed = 20;
    }
    /// <summary>
    /// 获取本地模型文件的版本号，规范为preciousmetal20190623172642000,即后17位为版本号
    /// </summary>
    private Int64 GetOldVersion(string fileName)
    {
        
        string localFileName = "";
        string ver="", tmp="";
        if (Directory.Exists(LocalFileMgr.Instance.LocalModelPath) == false)
        {
            Directory.CreateDirectory(LocalFileMgr.Instance.LocalModelPath);
        }
        DirectoryInfo dir = new DirectoryInfo(LocalFileMgr.Instance.LocalModelPath);
        FileInfo[] fileInfos = dir.GetFiles();
        foreach(FileInfo fileInfo in fileInfos)
        {
            if(fileInfo.FullName.Length>17)
            {
                localFileName = fileInfo.FullName.Substring(0, fileInfo.FullName.Length - 17);
                if(localFileName.Equals(fileName))
                {
                    ver = fileInfo.FullName.Substring(fileInfo.FullName.Length - 17);
                    foreach(char v in ver)
                    {
                        if (Char.IsDigit(v))
                            tmp += v;
                    }
                    return Int64.Parse(tmp);
                }
            }
        }
        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(AllowRotate)
        transform.Rotate(Axis, RotateSpeed * Time.deltaTime);
    }


    public void LoadPreciousMetal(string MetalId)
    {
        EventManager.Instance.AddEventListener(1001, SetPMValue);

        HttpManager.Instance.WebUrlBase = "http://www.bocdemo.cn";
        Hashtable ht = new Hashtable
        {
            { "", "" }
        };
        HttpManager.Instance.GetWebResult("/arps/viewPreciousMetal?MetalId="+MetalId, ht, 1001);

    }

    private void SetPMValue(object arg)
    {
        JsonData data = new JsonData(arg);
        // PreciousMetal PM = JsonConvert.DeserializeObject<PreciousMetal>(data);

        //注意判断空值
        this.MetalId = data["MetalId"].ToString();
        this.MetalName = data["MetalName"].ToString();
        this.Dimentions = data["Dimentions"].ToString();
        this.MadeofMaterial = data["MadeofMaterial"].ToString();
        this.IndustrialArt = data["IndustrialArt"].ToString();
        this.MainObject = data["MainObject"].ToString();
        this.GiftObject = data["GiftObject"].ToString();
        this.Weight = Double.Parse(data["Weight"].ToString());
        this.Price = Double.Parse(data["Price"].ToString());
        this.QRCodeUrl = data["QRCodeUrl"].ToString();
        this.Categary = data["Categary"].ToString();
        this.ModelUrl = data["ModelUrl"].ToString();
        //  this.Version = Double.Parse(data["Version"].ToString());
        string v = data["Version"].ToString();
        string tmp = "";
        foreach (char c in v)
        {
            if(Char.IsDigit(c))
            tmp += c;
        }
        this.Version = Int64.Parse(tmp);

        this.OldVersion = GetOldVersion(Path.GetFileName(this.ModelUrl));

    }

    private void LoadPreciousMetalModel()
    {
        string fileName = Path.GetFileName(this.ModelUrl);
        if (this.Version > this.OldVersion)
        {
            if (File.Exists(LocalFileMgr.Instance.LocalModelPath + fileName + this.OldVersion))
                File.Delete(LocalFileMgr.Instance.LocalModelPath+ fileName + this.OldVersion);//删除旧版本，减少本地存储

            HttpDownload.Instance.Download(this.ModelUrl, LocalFileMgr.Instance.LocalModelPath, fileName, LoadModel);
            
        }
        else//断点续传能判断本地是否存在文件，不存在或不完整则下载，下载完整后加载。
        {
            HttpDownload.Instance.Download(this.ModelUrl, LocalFileMgr.Instance.LocalModelPath, fileName, LoadModel);
        }


    }

    private void LoadModel()
    {
        //ResourcesManager.Instance.LoadAssetBundle("", "");

    }



    #region
    /// <summary>
    /// 重置位置
    /// </summary>
    private Vector3 initPos;
    private Vector3 initAgl;
    private Vector3 initScale;
    public void ResetPosition()
    {
        transform.position = initPos;
        transform.eulerAngles = initAgl;
        transform.localScale = initScale;
    }
    #endregion

    public void Scale(float scaleMultiplier)
    {
        transform.localScale *= scaleMultiplier;
    }

    public void Translate()
    {
       // transform.Translate(0.01f * gesture.DeltaX, 0, 0, Space.Self);
    }

    public void Rotate(Vector3 eulers,Space relativeTo)
    {
        transform.Rotate(eulers, relativeTo);
    }

   
}
