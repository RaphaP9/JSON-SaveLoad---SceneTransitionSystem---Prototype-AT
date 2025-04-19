using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

public abstract class JSONDataPersistenceManager<T> : MonoBehaviour, IDataPersistenceManager where T : class, new()
{
    [Header("Enablers")]
    [SerializeField] private bool enableDataPersistence;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private string dirPath;
    private IDataService dataService;
    private T persistentData;
    private List<IDataSaveLoader<T>> dataSaveLoaderObjects;

    public static event EventHandler OnDataLoadStart;
    public static event EventHandler OnDataLoadCompleted;

    public static event EventHandler OnDataSaveStart;
    public static event EventHandler OnDataSaveCompleted;

    protected void Awake()
    {
        SetSingleton();
        InitializeDataPersistenceManager();
    }

    protected void InitializeDataPersistenceManager()
    {
        dirPath = Application.persistentDataPath;

        if (useEncryption) dataService = new JSONNewtonSoftDataServiceEncryption(dirPath, fileName); //NewtonSoft Services
        else dataService = new JSONNewtonsoftDataServiceNoEncryption(dirPath, fileName);

        dataSaveLoaderObjects = FindAllDataSaveLoaderObjects();

        //LoadData(); Responsability delegated to General DataSaveLoader
    }


    protected List<IDataSaveLoader<T>> FindAllDataSaveLoaderObjects()
    {
        IEnumerable<IDataSaveLoader<T>> dataSaveLoaderObjectsNumerable = FindObjectsOfType<MonoBehaviour>().OfType<IDataSaveLoader<T>>();
        List<IDataSaveLoader<T>> dataSaveLoaderObjectsList = new List<IDataSaveLoader<T>>(dataSaveLoaderObjectsNumerable);

        return dataSaveLoaderObjectsList;
    }

    protected virtual void SetSingleton() { }

    #region Save Data
    public void SaveData()
    {
        if (!enableDataPersistence) return;

        OnDataSaveStartMethod();

        foreach (IDataSaveLoader<T> dataSaveLoaderObject in dataSaveLoaderObjects) //Pass data to other scripts so they can update it
        {
            dataSaveLoaderObject.SaveData(ref persistentData);
        }

        dataService.SaveData(persistentData); //Save data to file using data handler 

        OnDataSaveCompletedMethod();
    }

    public async Task SaveDataAsync()
    {
        if (!enableDataPersistence) return;

        OnDataSaveStartMethod();

        foreach (IDataSaveLoader<T> dataSaveLoaderObject in dataSaveLoaderObjects) //Pass data to other scripts so they can update it
        {
            dataSaveLoaderObject.SaveData(ref persistentData);
        }

        await dataService.SaveDataAsync(persistentData); //Save data to file using data handler 

        OnDataSaveCompletedMethod();
    }

    #endregion

    #region Load Data
    public void LoadData()
    {
        if (!enableDataPersistence) return;

        OnDataLoadStartMethod();

        persistentData = dataService.LoadData<T>(); //Load data from file using data handler

        if (persistentData == default || persistentData == null)
        {
            if(debug) Debug.Log("No data was found. Initializing data to defaults");

            NewData();
        }

        foreach (IDataSaveLoader<T> dataSaveLoaderObject in dataSaveLoaderObjects) //Push loaded data to scripts that need it
        {
            dataSaveLoaderObject.LoadData(persistentData);
        }

        OnDataLoadCompletedMethod();
    }

    public async Task LoadDataAsync()
    {
        if (!enableDataPersistence) return;

        OnDataLoadStartMethod();

        persistentData = await dataService.LoadDataAsync<T>(); //Load data from file using data handler

        if (persistentData == default || persistentData == null)
        {
            if (debug) Debug.Log("No data was found. Initializing data to defaults");

            NewData();
        }

        foreach (IDataSaveLoader<T> dataSaveLoaderObject in dataSaveLoaderObjects) //Push loaded data to scripts that need it
        {
            dataSaveLoaderObject.LoadData(persistentData);
        }

        OnDataLoadCompletedMethod();
    }

    #endregion

    protected void NewData() => persistentData = new T();

    public void DeleteGameData()
    {
        dirPath = Application.persistentDataPath;

        string path = Path.Combine(dirPath, fileName);

        if (!File.Exists(path))
        {
            Debug.Log("No data to delete");
            return;
        }

        File.Delete(path);
        Debug.Log("Data Deleted");
    }

    ////////////////////////////////////////////////////////////////////////
   
    protected virtual void OnDataLoadStartMethod()
    {
        OnDataLoadStart?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnDataLoadCompletedMethod()
    {
        OnDataLoadCompleted?.Invoke(this, EventArgs.Empty);
    }
    
    protected virtual void OnDataSaveStartMethod()
    {
        OnDataSaveStart?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnDataSaveCompletedMethod()
    {
        OnDataSaveCompleted?.Invoke(this, EventArgs.Empty); 
    }
}
