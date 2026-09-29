using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<GameObject> PlayerClicked;
    public event Action<GameObject> PlayerHoveredOver;

    #region Singleton
    private static PlayerInput instance;

    public static PlayerInput Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerInput();
            }
            return instance;
        }
    }

    private PlayerInput()
    {
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static bool HasInstance()
    {
        return instance != null;
    }
    #endregion

    private const float hoverCheckInterval = 0.2f;
    private float hoverCheckTimer;
    private void Update()
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    PlayerClicked?.Invoke(hit.transform.gameObject);
                }
            }
            hoverCheckTimer += Time.deltaTime;
            if(hoverCheckTimer>= hoverCheckInterval)
            {
                hoverCheckTimer = 0;
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    PlayerHoveredOver?.Invoke(hit.transform.gameObject);
                }
            }
        }
    }
}
