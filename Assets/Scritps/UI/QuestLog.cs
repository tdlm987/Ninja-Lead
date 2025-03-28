using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private Transform registerPanel;
    [SerializeField] private Transform loginPanel;


    [SerializeField] private Transform resetPasswordPanel;
    [SerializeField] private Transform sendOTPPanel;
    [SerializeField] private Transform OTPConfirmPanel;
    [SerializeField] private Transform newPasswordResetPanel;

    [SerializeField] private Transform Notification;

    [SerializeField] private Ease ease;

    private void Start()
    {
        DisplayDefault();
    }

    //Register
    public void GoToCreateAccount()
    {
        loginPanel.gameObject.SetActive(false);
        registerPanel.gameObject.SetActive(true);
        PanelUIEffectFX(registerPanel);
        RefreshAllInputFieldText(registerPanel.gameObject);
    }
    //Login
    public void GoToLoginAccount()
    {
        registerPanel.gameObject.SetActive(false);
        resetPasswordPanel.gameObject.SetActive(false);
        loginPanel.gameObject.SetActive(true);
        PanelUIEffectFX(loginPanel);
        RefreshAllInputFieldText(loginPanel.gameObject);
    }
    //Reset Password
    public void GoToResetPassword()
    {
        loginPanel.gameObject.SetActive(false);
        resetPasswordPanel.gameObject.SetActive(true);
        sendOTPPanel.gameObject.SetActive(true);
        PanelUIEffectFX(resetPasswordPanel);
        OTPConfirmPanel.gameObject.SetActive(false);
        newPasswordResetPanel.gameObject.SetActive(false);
        RefreshAllInputFieldText(resetPasswordPanel.gameObject);
    }
    public void BackToResendOTP()
    {
        sendOTPPanel.gameObject.SetActive(true);
        OTPConfirmPanel.gameObject.SetActive(false);
    }
    public void GoToOTPComfirm()
    {
        sendOTPPanel.gameObject.SetActive(false);
        OTPConfirmPanel.gameObject.SetActive(true);
        RefreshAllInputFieldText(resetPasswordPanel.gameObject);
    }
    public void GoToCreateNewPassword()
    {
        OTPConfirmPanel.gameObject.SetActive(false);
        newPasswordResetPanel.gameObject.SetActive(true);
        RefreshAllInputFieldText(resetPasswordPanel.gameObject);
    }
    public void IDontWantToResetPasswordAnymore()
    {
        resetPasswordPanel.gameObject.SetActive(false);
        loginPanel.gameObject.SetActive(true);
        PanelUIEffectFX(loginPanel);
    }
    private void DisplayDefault()
    {
        GoToLoginAccount();
    }
    public void RefreshAllInputFieldText(GameObject panel)
    {
        TMP_InputField[] listInputFields = panel.GetComponentsInChildren<TMP_InputField>();
        for (int i = 0; i < listInputFields.Length; i++)
            listInputFields[i].text = "";
    }
    private void PanelUIEffectFX(Transform transform)
    {
        transform.GetComponent<RectTransform>().localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f).SetEase(ease);
    }
    [SerializeField] private Ease notification_In;
    [SerializeField] private Ease notification_Out;
    public void DisplayNotification()
    {
        Notification.gameObject.SetActive(true);
        Notification.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        Notification.transform.GetChild(0).DOScale(Vector3.one, 1f).SetEase(notification_In);
    }
    public void HideNotification()
    {
        Notification.DOScale(Vector3.zero, 0.5f).SetEase(notification_Out).OnComplete(() =>
        {
            Notification.gameObject.SetActive(false);
            Notification.GetComponent<RectTransform>().localScale = Vector3.one;
            Notification.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
        });
    }
}
