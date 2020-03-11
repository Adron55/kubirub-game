using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class languageForRubicsCube : MonoBehaviour {

    public struct lang_Game
    {
        public string completePanel, volumeText, cameraText, soundsText, tweeningText, offlineMode, connErrorText, titleText, langBtnText, helpText, RYSureToQuit, yes, no, infoTitle, back,goToOnlineTxt;
    }

    public struct lang_Login
    {
        public string username, password, rememberMe, signIn, forgetPass;
    }

    public struct lang_Register
    {
        public string fullName, birthDate, mail, password, retryPassword, privacyPolicy, help, contact, createAccount, iAccept, and, termsConditions;
    }

    public struct lang_Discounts
    {
        public string discounts, oldCoupons, requestToSeller, deleteCoupon, yes, no, back, deletePopUpText, deletedToastText, feedbackSentSuccess, feedbackError, sendFeedbackForService, feedbackButton, cancel, send, enterText, commentSent, thankForRecommendation;
    }

    public struct lang_MyProfile
    {
        public string myProfile, editProfile, logOut, yes, no, finish, select, oldPass, newPass, retryNewPass, RYSureToLogOut, cancel, password;
    }

    public struct lang_BeforeRegister
    {
        public string haveAccount, createAccount, signUp, playWin, withoutRegister;
    }


    [HideInInspector]
    public lang_Game language_Game;
    public lang_BeforeRegister language_BeforeRegister;
    public lang_Login language_Login;
    public lang_Register language_Register;
    public lang_Discounts language_Discounts;
    public lang_MyProfile language_MyProfile;

    private void Start()
    {
        defineTexts();
    }


    public string[] getOffModeTxt()
    {
        string[] names = { language_Game.offlineMode, language_Game.goToOnlineTxt };
        return names;
    }
    public lang_Game defineTexts()
    {
        int l = PlayerPrefs.GetInt("Language");
        switch (l)
        {
            case 0:
                inEnglish();
                break;
            case 1:
                inRussian();
                break;
            case 2:
                inTurkish();
                break;
            case 3:
                inAzeri();
                break;
            default:
                inEnglish();
                break;
        }

        return (language_Game);
    }




    private void inEnglish()
    {
        lang_Game lang_Gamee = new lang_Game();
        lang_Gamee.completePanel = "Congratulations!";
        lang_Gamee.offlineMode = "You are in offline mode.\nIf you continue to play you will not win coupons.";
        lang_Gamee.connErrorText = "There is no internet connection.\nCheck your network.";
        lang_Gamee.volumeText = "volume";
        lang_Gamee.cameraText = "camera";
        lang_Gamee.soundsText = "sounds";
        lang_Gamee.tweeningText = "animation";
        lang_Gamee.titleText = "settings";
        lang_Gamee.langBtnText = "en";
        lang_Gamee.helpText = "help";
        lang_Gamee.RYSureToQuit = "Are you sure to quit?";
        lang_Gamee.yes = "YES";
        lang_Gamee.no = "NO";
        lang_Gamee.infoTitle = "companies";
        lang_Gamee.back = "back";
        lang_Gamee.goToOnlineTxt = "Go online!";
        language_Game = lang_Gamee;

        lang_Login lang_Loginn = new lang_Login();
        lang_Loginn.username = "login";
        lang_Loginn.password = "password";
        lang_Loginn.rememberMe = "Remember me";
        lang_Loginn.signIn = "Sign in";
        lang_Loginn.forgetPass = "Forget password?";
        language_Login = lang_Loginn;


        lang_Register lang_Registerr = new lang_Register();
        lang_Registerr.fullName = "full name";
        lang_Registerr.birthDate = "birthdate";
        lang_Registerr.mail = "e-mail";
        lang_Registerr.password = "password";
        lang_Registerr.retryPassword = "retry password";
        lang_Registerr.privacyPolicy = "Privacy Policy";
        lang_Registerr.help = "help";
        lang_Registerr.contact = "contact";
        lang_Registerr.createAccount = "Create account";
        lang_Registerr.iAccept = "I accept the ";
        lang_Registerr.and = " and the ";
        lang_Registerr.termsConditions = "Terms and Conditions";
        language_Register = lang_Registerr;

        lang_Discounts lang_Discountss = new lang_Discounts();
        lang_Discountss.discounts = "discounts";
        lang_Discountss.oldCoupons = "old сoupons";
        lang_Discountss.requestToSeller = "request to seller";
        lang_Discountss.deleteCoupon = "delete";
        lang_Discountss.yes = "YES";
        lang_Discountss.no = "NO";
        lang_Discountss.back = "back";
        lang_Discountss.deletePopUpText = "Do you want to delete this coupon?";
        lang_Discountss.deletedToastText = "Coupon is deleted!";
 
        lang_Discountss.feedbackSentSuccess = "Sent successfully!";
        lang_Discountss.feedbackError = "Error";
        lang_Discountss.sendFeedbackForService = "Send feedback for service or products";
        lang_Discountss.feedbackButton = "feedback";
        lang_Discountss.cancel = "cancel";
        lang_Discountss.send = "SEND";
        lang_Discountss.enterText = "Enter text...";
        lang_Discountss.commentSent = "Your comment is sent";
        lang_Discountss.thankForRecommendation = "Thanks for recommendation";


        language_Discounts = lang_Discountss;

        lang_MyProfile lang_MyProfilee = new lang_MyProfile();
        lang_MyProfilee.myProfile = "my profile";
        lang_MyProfilee.editProfile = "Edit profile";
        lang_MyProfilee.logOut = "log out";
        lang_MyProfilee.yes = "YES";
        lang_MyProfilee.no = "NO";
        lang_MyProfilee.finish = "Finish";
        lang_MyProfilee.select = "select";
        lang_MyProfilee.oldPass = "current password";
        lang_MyProfilee.newPass = "new password";
        lang_MyProfilee.retryNewPass = "confirm new password";
        lang_MyProfilee.RYSureToLogOut = "Do you want to log out?";
        lang_MyProfilee.cancel = "cancel";
        lang_MyProfilee.password = "password";
        language_MyProfile = lang_MyProfilee;



        lang_BeforeRegister lang_BeforeRegisterr = new lang_BeforeRegister();
        lang_BeforeRegisterr.haveAccount = "I have an account";
        lang_BeforeRegisterr.createAccount = "Create new account";
        lang_BeforeRegisterr.signUp = "Sign up";
        lang_BeforeRegisterr.playWin = "Play & Win";
        lang_BeforeRegisterr.withoutRegister = "continue without registration";
        language_BeforeRegister = lang_BeforeRegisterr;
        //signupBTN.text = "Sign up";
        //comingSoonText.text = "Coming soon";
        //##**rehman**##(ola bilsin yeni versiyada olsun ya olmasin)//passLenErrorText.text = "Password or email isn't correct.";
        //##**rehman**##(ola bilsin yeni versiyada olsun ya olmasin)//connectionErrorText.text = "You are not connected to the internet.";
    }
    private void inRussian()
    {
        lang_Game lang_Gamee = new lang_Game();
        lang_Gamee.completePanel = "Поздравления!";
        lang_Gamee.offlineMode = "Вы в режиме оффлайн.\nЕсли вы продолжите играть,вы не выиграете купонов.";
        lang_Gamee.connErrorText = "Нет интернет соединения.\nПроверьте сеть.";
        lang_Gamee.volumeText = "громкость";
        lang_Gamee.cameraText = "kамера";
        lang_Gamee.soundsText = "звуки";
        lang_Gamee.tweeningText = "aнимация";
        lang_Gamee.titleText = "настройки";
        lang_Gamee.langBtnText = "ru";
        lang_Gamee.helpText = "помощь";
        lang_Gamee.RYSureToQuit = "Вы уверены, что бросили?";
        lang_Gamee.yes = "ДА";
        lang_Gamee.no = "НЕТ";
        lang_Gamee.infoTitle = "компания";
        lang_Gamee.back = "назад";
        lang_Gamee.goToOnlineTxt = "выйти в онлайн!";
        language_Game = lang_Gamee;


        lang_Login lang_Loginn = new lang_Login();
        lang_Loginn.username = "эл.адрес";
        lang_Loginn.password = "пароль";
        lang_Loginn.rememberMe = "Запомнить меня";
        lang_Loginn.signIn = "Вход";
        lang_Loginn.forgetPass = "Забыли пароль?";
        language_Login = lang_Loginn;

        lang_Register lang_Registerr = new lang_Register();
        lang_Registerr.fullName = "полное имя";
        lang_Registerr.birthDate = "день рождения";
        lang_Registerr.mail = "эл.адрес";
        lang_Registerr.password = "пароль";
        lang_Registerr.retryPassword = "пароль";
        lang_Registerr.privacyPolicy = "Политику использования данных";
        lang_Registerr.help = "помощь";
        lang_Registerr.contact = "контакт";
        lang_Registerr.createAccount = "Создать аккаунт";
        lang_Registerr.iAccept = "Я принимаю ";
        lang_Registerr.and = " и ";
        lang_Registerr.termsConditions = " Правила и Условия";
        language_Register = lang_Registerr;

        lang_Discounts lang_Discountss = new lang_Discounts();
        lang_Discountss.discounts = "cкидки";
        lang_Discountss.oldCoupons = "cтарые купоны";
        lang_Discountss.requestToSeller = "bаш заказ";
        lang_Discountss.deleteCoupon = "удалять";
        lang_Discountss.yes = "ДА";
        lang_Discountss.no = "НЕТ";
        lang_Discountss.back = "назад";
        lang_Discountss.deletePopUpText = "Хотите ли вы удалить ваш купон?";
        lang_Discountss.deletedToastText = "Купон удален!";

        lang_Discountss.feedbackSentSuccess = "Успешно отправлено!";
        lang_Discountss.feedbackError = "Oшибка";
        lang_Discountss.sendFeedbackForService = "Отправить отзыв о товаре или услуге";
        lang_Discountss.feedbackButton = "Обратная связь";
        lang_Discountss.cancel = "oтмена";
        lang_Discountss.send = "ОТПРАВИТЬ";
        lang_Discountss.enterText = "Введите текст...";
        lang_Discountss.commentSent = "Ваш комментарий отправлен";
        lang_Discountss.thankForRecommendation = "Спасибо за рекомендацию";
        language_Discounts = lang_Discountss;

        lang_MyProfile lang_MyProfilee = new lang_MyProfile();
        lang_MyProfilee.myProfile = "mой профиль";
        lang_MyProfilee.editProfile = "Pедактировать профиль";
        lang_MyProfilee.logOut = "выйти";
        lang_MyProfilee.yes = "ДA";
        lang_MyProfilee.no = "НЕТ";
        lang_MyProfilee.finish = "Конец";
        lang_MyProfilee.select = "bыберите";
        lang_MyProfilee.oldPass = "действующий пароль";
        lang_MyProfilee.newPass = "новый пароль";
        lang_MyProfilee.retryNewPass = "новый пароль";
        lang_MyProfilee.RYSureToLogOut = "Хотите ли вы покинуть этот аккаунт?";
        lang_MyProfilee.cancel = "oтмена";
        lang_MyProfilee.password = "пароль";
        language_MyProfile = lang_MyProfilee;

        lang_BeforeRegister lang_BeforeRegisterr = new lang_BeforeRegister();
        lang_BeforeRegisterr.haveAccount = "У меня есть аккаунт";
        lang_BeforeRegisterr.createAccount = "Регистрация";
        lang_BeforeRegisterr.signUp = "Подписаться";
        lang_BeforeRegisterr.playWin = "Играть & Выиграть";
        lang_BeforeRegisterr.withoutRegister = "продолжите без регистрации";
        language_BeforeRegister = lang_BeforeRegisterr;
    }
    private void inTurkish()
    {
        lang_Game lang_Gamee = new lang_Game();
        lang_Gamee.completePanel = "Tebrikler!";
        lang_Gamee.offlineMode = "Çevrimdışı moddasınız.\nEğer oynamaya devam edersen kupon kazanamazsın.";
        lang_Gamee.connErrorText = "İnternet bağlantısı yok.\nAğınızı kontrol edin.";
        lang_Gamee.volumeText = "ses ayarı";
        lang_Gamee.cameraText = "kamera ayarı";
        lang_Gamee.soundsText = "ses";
        lang_Gamee.tweeningText = "animasyon";
        lang_Gamee.titleText = "parametreler";
        lang_Gamee.langBtnText = "tu";
        lang_Gamee.helpText = "yardım";
        lang_Gamee.RYSureToQuit = "Çıkacağından emin misin?";
        lang_Gamee.yes = "EVET";
        lang_Gamee.no = "HAYIR";
        lang_Gamee.infoTitle = "şirketler";
        lang_Gamee.back = "geriye";
        lang_Gamee.goToOnlineTxt = "çevrimiçi ol!";
        language_Game = lang_Gamee;


        lang_Login lang_Loginn = new lang_Login();
        lang_Loginn.username = "e-posta";
        lang_Loginn.password = "şifre";
        lang_Loginn.rememberMe = "Beni hatırla";
        lang_Loginn.signIn = "Giriş yap";
        lang_Loginn.forgetPass = "Şifreni unuttun mu?";
        language_Login = lang_Loginn;

        lang_Register lang_Registerr = new lang_Register();
        lang_Registerr.fullName = "tam ad";
        lang_Registerr.birthDate = "yaş";
        lang_Registerr.mail = "e-posta";
        lang_Registerr.password = "şifre";
        lang_Registerr.retryPassword = "şifre";
        lang_Registerr.privacyPolicy = "Güvenlik koşullarınızı";
        lang_Registerr.help = "yardım";
        lang_Registerr.contact = "bağlantı";
        lang_Registerr.createAccount = "Kayıt ol";
        lang_Registerr.iAccept = "Sizin tüm ";
        lang_Registerr.and = " kabul ediyoruz.";
        lang_Registerr.termsConditions = "";
        language_Register = lang_Registerr;

        lang_Discounts lang_Discountss = new lang_Discounts();
        lang_Discountss.discounts = "indirimler";
        lang_Discountss.oldCoupons = "eski kuponlar";
        lang_Discountss.requestToSeller = "siparişiniz";
        lang_Discountss.deleteCoupon = "sil";
        lang_Discountss.yes = "EVET";
        lang_Discountss.no = "HAYİR";
        lang_Discountss.back = "geriye";
        lang_Discountss.deletePopUpText = "Bu kuponu silmek istiyormusunuz?";
        lang_Discountss.deletedToastText = "Kupon silindi!";

        lang_Discountss.feedbackSentSuccess = "Başarıyla gönderildi!";
        lang_Discountss.feedbackError = "Hata";
        lang_Discountss.sendFeedbackForService = "Hizmet veya ürünler için geri bildirim gönderin";
        lang_Discountss.feedbackButton = "geri bildirim";
        lang_Discountss.cancel = "iptal";
        lang_Discountss.send = "GÖNDER";
        lang_Discountss.enterText = "Metin gir...";
        lang_Discountss.commentSent = "Yorumunuz gönderildi";
        lang_Discountss.thankForRecommendation = "Öneri için teşekkürler";
        language_Discounts = lang_Discountss;

        lang_MyProfile lang_MyProfilee = new lang_MyProfile();
        lang_MyProfilee.myProfile = "profil";
        lang_MyProfilee.editProfile = "Profili düzenle";
        lang_MyProfilee.logOut = "çıkış yap";
        lang_MyProfilee.yes = "EVET";
        lang_MyProfilee.no = "HAYIR";
        lang_MyProfilee.finish = "Kayıt";
        lang_MyProfilee.select = "ayarla";
        lang_MyProfilee.oldPass = "şifreniz";
        lang_MyProfilee.newPass = "yeni şifreniz";
        lang_MyProfilee.retryNewPass = "yeni şifreniz";
        lang_MyProfilee.RYSureToLogOut = "Profilden çıkmak istediğinize eminmisiniz?";
        lang_MyProfilee.cancel = "iptal";
        lang_MyProfilee.password = "şifre";
        language_MyProfile = lang_MyProfilee;

        lang_BeforeRegister lang_BeforeRegisterr = new lang_BeforeRegister();
        lang_BeforeRegisterr.haveAccount = "Hesabim var";
        lang_BeforeRegisterr.createAccount = "Yeni hesab yarat";
        lang_BeforeRegisterr.signUp = "Kaydol";
        lang_BeforeRegisterr.playWin = "Oyna & Kazan";
        lang_BeforeRegisterr.withoutRegister = "kayıt olmadan devam et";
        language_BeforeRegister = lang_BeforeRegisterr;
    }
    private void inAzeri()
    {
        lang_Game lang_Gamee = new lang_Game();
        lang_Gamee.completePanel = "Təbriklər!";
        lang_Gamee.offlineMode = "İnternetə qoşulmamısınız.\nOyuna davam etsəniz, kupon qazana bilməzsiniz.";
        lang_Gamee.connErrorText = "İnternet bağlantısı yoxdur.\nŞəbəkənizi yoxlayın.";
        lang_Gamee.volumeText = "səs parametri";
        lang_Gamee.cameraText = "kamera parametri";
        lang_Gamee.soundsText = "səs";
        lang_Gamee.tweeningText = "Animasiya";
        lang_Gamee.titleText = "tənzimləmələr";
        lang_Gamee.langBtnText = "az";
        lang_Gamee.helpText = "kömək";
        lang_Gamee.RYSureToQuit = "Oyundan çıxmaq istəyirsinizmi?";
        lang_Gamee.yes = "BƏLİ";
        lang_Gamee.no = "XEYR";
        lang_Gamee.infoTitle = "şirkətlər";
        lang_Gamee.back = "geriye";
        lang_Gamee.goToOnlineTxt = "onlayna get!";
        language_Game = lang_Gamee;


        lang_Login lang_Loginn = new lang_Login();
        lang_Loginn.username = "email";
        lang_Loginn.password = "parol";
        lang_Loginn.rememberMe = "Məni xatırla";
        lang_Loginn.signIn = "Daxil ol";
        lang_Loginn.forgetPass = "Parolu unutdun?";
        language_Login = lang_Loginn;

        lang_Register lang_Registerr = new lang_Register();
        lang_Registerr.fullName = "tam ad";
        lang_Registerr.birthDate = "yaş";
        lang_Registerr.mail = "e-poçt";
        lang_Registerr.password = "parol";
        lang_Registerr.retryPassword = "parol";
        lang_Registerr.privacyPolicy = "İstifadəçi qaydalarını";
        lang_Registerr.help = "kömək";
        lang_Registerr.contact = "əlaqə";
        lang_Registerr.createAccount = "Qeydiyyat";
        lang_Registerr.iAccept = "Mən sizin ";
        lang_Registerr.and = " qəbul edirəm.";
        lang_Registerr.termsConditions = "";
        language_Register = lang_Registerr;

        lang_Discounts lang_Discountss = new lang_Discounts();
        lang_Discountss.discounts = "endirimlər";
        lang_Discountss.oldCoupons = "köhnə kuponlar";
        lang_Discountss.requestToSeller = "sifarişiniz";
        lang_Discountss.deleteCoupon = "sil";
        lang_Discountss.yes = "BƏLİ";
        lang_Discountss.no = "XEYR";
        lang_Discountss.back = "geriyə";
        lang_Discountss.deletePopUpText = "Bu kuponu silmək istəyirsinizmi?";
        lang_Discountss.deletedToastText = "Kupon silindi!";

        lang_Discountss.feedbackSentSuccess = "Uğurla göndərildi!";
        lang_Discountss.feedbackError = "Xəta";
        lang_Discountss.sendFeedbackForService = "Xidmət və ya məhsullar üçün rəy göndərin";
        lang_Discountss.feedbackButton = "geribildirim";
        lang_Discountss.cancel = "ləğv et";
        lang_Discountss.send = "GÖNDƏR";
        lang_Discountss.enterText = "Mətn daxil edin...";
        lang_Discountss.commentSent = "Şərhiniz göndərildi";
        lang_Discountss.thankForRecommendation = "Tövsiyə üçün təşəkkürlər";
        language_Discounts = lang_Discountss;

        lang_MyProfile lang_MyProfilee = new lang_MyProfile();
        lang_MyProfilee.myProfile = "profil";
        lang_MyProfilee.editProfile = "Profilə düzəliş et";
        lang_MyProfilee.logOut = "profilden cixiş";
        lang_MyProfilee.yes = "BƏLİ";
        lang_MyProfilee.no = "XEYR";
        lang_MyProfilee.finish = "Saxla";
        lang_MyProfilee.select = "seç";
        lang_MyProfilee.oldPass = "cari parol";
        lang_MyProfilee.newPass = "yeni parol";
        lang_MyProfilee.retryNewPass = "yeni parol";
        lang_MyProfilee.RYSureToLogOut = "Profildən çıxmaq istəyirsinizmi?";
        lang_MyProfilee.cancel = "ləğv et";
        lang_MyProfilee.password = "parol";
        language_MyProfile = lang_MyProfilee;

        lang_BeforeRegister lang_BeforeRegisterr = new lang_BeforeRegister();
        lang_BeforeRegisterr.haveAccount = "Hesabim var";
        lang_BeforeRegisterr.createAccount = "Yeni hesab yarat";
        lang_BeforeRegisterr.signUp = "Qeydiyyatdan keç";
        lang_BeforeRegisterr.playWin = "Oyna & Qazan";
        lang_BeforeRegisterr.withoutRegister = "qeydiyyat olmadan davam et";
        language_BeforeRegister = lang_BeforeRegisterr;
    }

}
