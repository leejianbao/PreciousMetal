//===================
//描述：
//作者：lijianbao
//创建时间：2019-06-19 10:11:17
//版本：V1.0
//==================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;

public class QRCode : Singleton<QRCode>
{
    
    public void GenQRcode(RawImage QRImage,string URL)
    {
        Texture2D encoded = new Texture2D(256, 256);
        var textForEncoding = URL;
        if (textForEncoding != null)
        {
            var color32 = Encode(textForEncoding, encoded.width, encoded.height);
            encoded.SetPixels32(color32);
            encoded.Apply();


            Texture2D encoded1;
            encoded1 = new Texture2D(198, 198);
            encoded1.SetPixels(encoded.GetPixels(32, 32, 198, 198));
            encoded1.Apply();


            QRImage.texture = encoded1;
        }

    }

    public Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }


}
