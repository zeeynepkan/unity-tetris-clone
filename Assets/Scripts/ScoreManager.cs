using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score = 0; //skorun başlangıcı
    private int raws; //satırlar
    public int level = 1; //seviyeyi 1 den başlatıyoruz 


    public int rawNumberInLevel = 5; //seviyedeki satır sayısı

    private int minRaw = 1;
    private int maxRaw = 4;


    public TextMeshProUGUI rawTxt; //text mesh prolarla textleri kontrol edeceğiz
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI scoreTxt;

    public bool IsLevelUp = false;//level geçildimi başta false olmalı geçildiği zaman true yapacağız
    



    private void Start()
    {
        ResetFNC();
    }


    public void ResetFNC()
    {

        level = 1;
        raws = rawNumberInLevel * level;
        TextUpdateFNC();
    }


    public void RawScore(int n)
    {
        IsLevelUp = false;
        n = Mathf.Clamp(n,minRaw,maxRaw);//satır skoru min satır ile max satır arasındadır


        switch(n)
        {

            case 1:
            score += 30 * level;
            break;


            case 2:
            score += 80 * level;
            break;

            case 3:
            score += 150 * level;
            break;


            case 4:
            score += 500 * level;
            break;

        }

        raws -= n;
        if(raws <= 0)//satır sayısı 0  sa level atlayacak
        {
            LevelUpFNC();
        }

        TextUpdateFNC();
    }

    void TextUpdateFNC()
    {
        if (scoreTxt)
        {
            scoreTxt.text = PutZeroFirstFNC(score,numberOfDigits:5);
        }

        if (levelTxt)
        {
            levelTxt.text = level.ToString();
        }

        if (rawTxt)
        {
            rawTxt.text = raws.ToString();
        }
    }

    string PutZeroFirstFNC(int score, int numberOfDigits)//skor ve rakam sayılarıyla az gelirse başına sıfır ekleyeceğiz
    {
        string scoreStr = score.ToString();

        while (scoreStr.Length < numberOfDigits)
        {
            scoreStr = "0" + scoreStr;
        }

        return scoreStr;

    }


    public void LevelUpFNC()//level atlama fonksiyonu
    {
        level++;
        raws = rawNumberInLevel * level;
        IsLevelUp = true;//LEVELİN GEÇİLİP GEÇİLMEDİĞİNĞ BURADA KONTROL EDECEĞİZ


    }
    
}
