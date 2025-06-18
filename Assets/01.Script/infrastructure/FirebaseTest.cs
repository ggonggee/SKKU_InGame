using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseFirestore _db;

    private void Start()
    {
        Init();
    }


    // ���̾�̽� �� ������Ʈ�� ����
    private void Init()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("���̾�̽� ���ῡ �����߽��ϴ�.");

                _app = FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;

                Login();
            }
            else
            {
                Debug.LogError($"���̾�̽� ���� �����߽��ϴ�. ${dependencyStatus}");
            }
        });
    }

    private void Register()
    {
        var email = "teemo@gmail.com";
        var password = "123456";

        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"ȸ�����Կ� �����߽��ϴ�: ${task.Exception.Message}");
                return;
            }

            // Firebase user has been created.
            var result = task.Result;
            Debug.LogFormat("ȸ�����Կ� �����߽��ϴ�: {0} ({1})", result.User.DisplayName, result.User.UserId);
        });
    }

    private void Login()
    {
        var email = "teemo@gmail.com";
        var password = "123456";

        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"�α��ο� �����߽��ϴ�: {task.Exception.Message}");
                return;
            }

            var result = task.Result;
            Debug.LogFormat("�α��ο� �����߽��ϴ�: {0} ({1})", result.User.DisplayName, result.User.UserId);

            NicknameChange();

            AddMyRanking();
            GetMyRanking();
        });
    }

    private void NicknameChange()
    {
        var user = _auth.CurrentUser;

        if (user == null) return;

        var profile = new UserProfile
        {
            DisplayName = "teemo"
        };

        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"�г��� ���濡 �����߽��ϴ�: {task.Exception.Message}");
                return;
            }

            Debug.Log("�г��� ���濡 �����߽��ϴ�.");
        });
    }

    private void GetProfile()
    {
        var user = _auth.CurrentUser;
        if (user == null) return;

        var nickname = user.DisplayName;
        var email = user.Email;

        var account = new Account(email, nickname, "firebase");
    }


    private void AddMyRanking()
    {
        var ranking = new Ranking("huhuhu@gmail.com", "������", 7900);

        var rankingDict = new Dictionary<string, object>
        {
            { "Email", ranking.Email },
            { "Nickname", ranking.Nickname },
            { "Score", ranking.Score }
        };


        _db.Collection("rankings").AddAsync(rankingDict).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"������ �߰��� �����߽��ϴ�.: {task.Exception.Message}");
                return;
            }
        });

        _db.Collection("rankings").Document(ranking.Email).SetAsync(rankingDict).ContinueWithOnMainThread(task =>
        {
            Debug.Log(string.Format("Added or Updated document with ID: {0}.", task.Id));
        });
    }

    private void GetMyRanking()
    {
        var email = "huhuhu@gmail.com"; // ID ����

        var docRef = _db.Collection("rankings").Document(email);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            var snapshot = task.Result;

            if (snapshot.Exists)
            {
                Debug.Log(string.Format("Document data for {0} document:", snapshot.Id));
                var rankingDict = snapshot.ToDictionary();
                foreach (var pair in rankingDict)
                {
                    Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(string.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }
}