using TMPro;
using UnityEngine;

public class PopupBank : MonoBehaviour
{
    [Header("UI")]
    public GameObject atmUI;
    public GameObject depositUI;
    public GameObject withdrawalUI;
    public GameObject notEnoughUI;

    [Header("Value Input")]
    public TMP_InputField depositInputValue;    // ���� �Է��� �Ա� ��
    public TMP_InputField withdrawalInputValue; // ���� �Է��� ��� ��

    [Header("Text")]
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;

    private void Start()
    {
        Refresh();
    }

    public void OnDepositUI()           // �Ա�UI�� ����
    {
        atmUI.SetActive(false);
        depositUI.SetActive(true);
    }

    public void OnWithdrawalUI()        // ���UI�� ����
    {
        atmUI.SetActive(false);
        withdrawalUI.SetActive(true);
    }

    public void OnBackUI()              // �޴��� ���ư���
    {
        atmUI.SetActive(true);
        depositUI.SetActive(false);
        withdrawalUI.SetActive(false);
    }

    public void OnDeposit(int amount)   // �Ա��ϱ�
    {
        if (GameManager.Instance.userData.cash - amount < 0)
        {
            MessageNotEnough();
            return;
        }
        GameManager.Instance.userData.balance += amount;
        GameManager.Instance.userData.cash -= amount;
        GameManager.Instance.SaveUserData();
        Refresh();
    }

    public void OnWithdrawal(int amount)// ����ϱ�
    {
        if (GameManager.Instance.userData.balance - amount < 0)
        {
            MessageNotEnough();
            return;
        }
        GameManager.Instance.userData.balance -= amount;
        GameManager.Instance.userData.cash += amount;
        GameManager.Instance.SaveUserData();
        Refresh();
    }

    public void OnInputDeposit()        // ���� �Է� �Ա��ϱ�
    {
        int amount = int.Parse(depositInputValue.text);
        if (GameManager.Instance.userData.cash - amount < 0)
        {
            MessageNotEnough();
            return;
        }
        OnDeposit(amount);
    }

    public void OnInputWithdrawal()     // ���� �Է� ����ϱ�
    {
        int amount = int.Parse(withdrawalInputValue.text);
        if (GameManager.Instance.userData.balance - amount < 0)
        {
            MessageNotEnough();
            return;
        }
        OnWithdrawal(amount);
    }

    public void MessageNotEnough()          // �ܾ� ���� �˾�â ����
    {
        notEnoughUI.SetActive(true);
    }

    public void CloseNotEnoughMessage()     // �ܾ� ���� �˾�â �ݱ�
    {
        notEnoughUI.SetActive(false);
    }

    public void Refresh()                   // �ܾ� ������Ʈ�ϱ�
    {
        userNameText.text = GameManager.Instance.userData.userName;
        cashText.text = string.Format("{0:N0}", GameManager.Instance.userData.cash);
        balanceText.text = string.Format("{0:N0}", GameManager.Instance.userData.balance);
    }
}
