using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelManager : MonoBehaviour {
    private string _LevelDirectoryPath = "/levels/";
    private DirectoryInfo _levelDirectory;
    private List <FileInfo> _levels;


    private void GetLevels()
    {
        _levelDirectory = new DirectoryInfo(Application.dataPath + _LevelDirectoryPath);
        _levels = new List<FileInfo>(_levelDirectory.GetFiles("*.lvl"));
    }

    private void LoadLevel()
    {
    }

    private void Awake()
    {
        GetLevels();
    }
}
