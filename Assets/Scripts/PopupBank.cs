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
    public TMP_InputField depositInputValue;    // 직접 입력한 입금 값
    public TMP_InputField withdrawalInputValue; // 직접 입력한 출금 값

    [Header("Text")]
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;

    private void Start()
    {
        Refresh();
    }

    public void OnDepositUI()           // 입금UI로 가기
    {
        atmUI.SetActive(false);
        depositUI.SetActive(true);
    }

    public void OnWithdrawalUI()        // 출금UI로 가기
    {
        atmUI.SetActive(false);
        withdrawalUI.SetActive(true);
    }

    public void OnBackUI()              // 메뉴로 돌아가기
    {
        atmUI.SetActive(true);
        depositUI.SetActive(false);
        withdrawalUI.SetActive(false);
    }

    public void OnDeposit(int amount)   // 입금하기
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

    public void OnWithdrawal(int amount)// 출금하기
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

    public void OnInputDeposit()        // 직접 입력 입금하기
    {
        int amount = int.Parse(depositInputValue.text);
        if (GameManager.Instance.userData.cash - amount < 0)
        {
            MessageNotEnough();
            return;
        }
        OnDeposit(amount);
    }

    public void OnInputWithdrawal()     // 직접 입력 출금하기
    {
        int amount = int.Parse(withdrawalInputValue.text);
        if (GameManager.Instance.userData.balance - amount < 0)
        {
            MessageNotEnough();
            return;
        }
        OnWithdrawal(amount);
    }

    public void MessageNotEnough()          // 잔액 부족 팝업창 띄우기
    {
        notEnoughUI.SetActive(true);
    }

    public void CloseNotEnoughMessage()     // 잔액 부족 팝업창 닫기
    {
        notEnoughUI.SetActive(false);
    }

    public void Refresh()                   // 잔액 업데이트하기
    {
        userNameText.text = GameManager.Instance.userData.userName;
        cashText.text = string.Format("{0:N0}", GameManager.Instance.userData.cash);
        balanceText.text = string.Format("{0:N0}", GameManager.Instance.userData.balance);
    }
}
