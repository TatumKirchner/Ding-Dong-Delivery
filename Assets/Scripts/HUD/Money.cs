using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField] 
    private Text m_money;
    public float m_cash = 3.50f;
    private EmployeeUpgrades employeeUpgrades;

    private void Start()
    {
        employeeUpgrades = GetComponent<EmployeeUpgrades>();
    }

    /* 
     * Todo
     * Move this to an event so it is not running in update
     */
    private void Update()
    {
        m_money.text = string.Format("{0:C}", m_cash);
    }

    /*
     * Call to add money to the player. The amount is calculated from the cash float passed in multiplied by the employee upgrade multiplier
     */
    public void AddMoney(float cash)
    {
        switch (employeeUpgrades.numberOfEmployees)
        {
            case 0:
                m_cash += cash;
                break;
            case 1:
                m_cash += cash * employeeUpgrades.multiplyer;
                break;
            case 2:
                m_cash += cash * employeeUpgrades.multiplyer;
                break;
            case 3:
                m_cash += cash * employeeUpgrades.multiplyer;
                break;
            case 4:
                m_cash += cash * employeeUpgrades.multiplyer;
                break;
            case 5:
                m_cash += cash * employeeUpgrades.multiplyer;
                break;
        }
        
    }

    /*
     * Call to remove money from the player
     */
    public void SpendMoney(float cash)
    {
        m_cash -= cash;
    }

}
