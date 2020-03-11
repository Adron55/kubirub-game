using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LangSetterForDiscounts : MonoBehaviour
{
    public Text discounts, oldCoupons, oldCouponsButton, requestToSeller, deleteCoupon, yes, no, back, deletePopUpText, feedbackButton, cancel, send;
    public TMP_Text sendFeedbackForService, enterText, commentSent, thankForRecommendation;
    public static string deletedToastText, feedbackSentSuccess, feedbackError;

    // Start is called before the first frame update
    void Start()
    {
        languageForRubicsCube.lang_Discounts language = GameObject.Find("GameobjectForSavingİMG").GetComponent<languageForRubicsCube>().language_Discounts;

        deletedToastText = language.deletedToastText;
        //feedbackSentSuccess = , feedbackError

        discounts.text = language.discounts;
        oldCoupons.text = language.oldCoupons;
        oldCouponsButton.text = language.oldCoupons;
        requestToSeller.text = language.requestToSeller;
        deleteCoupon.text = language.deleteCoupon;
        yes.text = language.yes;
        no.text = language.no;
        back.text = language.back;
        deletePopUpText.text = language.deletePopUpText;

        feedbackSentSuccess = language.feedbackSentSuccess;
        feedbackError = language.feedbackError;
        sendFeedbackForService.text = language.sendFeedbackForService;
        feedbackButton.text = language.feedbackButton;
        cancel.text = language.cancel;
        send.text = language.send;
        enterText.text = language.enterText;
        commentSent.text = language.commentSent;
        thankForRecommendation.text = language.thankForRecommendation;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
