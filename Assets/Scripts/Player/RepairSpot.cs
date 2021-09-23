using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RepairSpot : MonoBehaviour
{
    public float m_waitTime = 5f;

    private GameObject gameManager;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] GameObject buttonOne;
    GameObject orderMenu;

    IsPaused isPaused;

    public bool shopOpen = false;
    private bool playerInShop = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        orderMenu = GameObject.Find("Order Menu");
        isPaused = gameManager.GetComponent<IsPaused>();
    }

    //When the player enters the shop open the shop menu
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !playerInShop)
        {
            playerInShop = true;
            StartCoroutine(OpenMenu());
        }
    }

    //When the player leaves the shop stop opening the menu if it hasn't already opened.
    //Also set the playerInShop bool so the menu doesn't reopen after closing the menu.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInShop = false;
            StopAllCoroutines();
        }
    }

    //Opens the shop menu
    IEnumerator OpenMenu()
    {
        yield return new WaitForSeconds(m_waitTime);
        shopOpen = true;
        isPaused.isPaused = true;
        shopMenu.SetActive(true);
        orderMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonOne);
    }

    //Closes the shop menu
    public void Close()
    {
        isPaused.isPaused = false;
        shopMenu.SetActive(false);
        orderMenu.SetActive(true);
        shopOpen = false;
    }
}
