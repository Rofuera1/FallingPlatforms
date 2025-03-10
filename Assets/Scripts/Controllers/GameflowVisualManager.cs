using System.Collections;
using UnityEngine;

public class GameflowVisualManager : MonoBehaviour
{
    public IEnumerator NewLoop()
    {
        Debug.Log("��� ��� - ��� �����");
        yield return null;
    }
    public IEnumerator ColorChooser(ColorsOnCurrentLoop Colors)
    {
        Debug.Log("������� ���� - " + Colors.Colors[0]);
        yield return null;
    }

    public IEnumerator DelayBeforeFalling()
    {
        Debug.Log("���� �� ������ ��� ����� 5 ������");
        yield return new WaitForSeconds(5f); // Move to scriptables
    }
    public IEnumerator Falling()
    {
        Debug.Log("�� ���, ��� �����(");
        yield return null;
    }
    public IEnumerator DelayAfterFalling()
    {
        Debug.Log("������ ����������� 5 ���");
        yield return new WaitForSeconds(2f); // Move to scriptables
    }

    public void OnEndGame(bool didWin)
    {
        Debug.Log("Wow! You won? " + didWin);
    }
}
