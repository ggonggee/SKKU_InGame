using UnityEngine;
    public enum ECurrencyType
    {
        Gold,
        Diamonde,

        Count,
    }

public class Currency 
{
    // ������ Ŭ������ ����:
    // 1. ǥ������ �����Ѵ�.
    // -> ȭ���� ������ �� ��� ǥ���� �� �ִ�.
    // 2. ���Ἲ�� �����ȴ�. (���Ἲ: �������� ��Ȯ��/�ϰ���/��ȿ��)
    // -> ���� ����: 0 �̸� ����, ������ ���� ����
    // 3. �����Ϳ� �����͸� �ٷ�� ������ �����ִ�. -> �������� ����.

    // �ڱ� �������� �ڵ尡 �ȴ�. (��ȹ���� �ǰ��� �ڵ尡 �ȴ�.)
    // ������(��ȹ��) ������ �Ͼ�� �ڵ忡 �ݿ��ϱ� ����. 

    // ȭ�� '������' (������, ����, ����, ��ȹ���� �������� �ۼ��Ѵ�: ��ȹ�ڶ� ���� ���ؾ� �Ѵ�.)

    private ECurrencyType _type;
    public ECurrencyType Type => _type;

    private int _value;
    public int Value => _value;


    //������

    public Currency(ECurrencyType type, int value)
    {
        _type = type;
        _value = value;
    }

    //���ϱ� Add

    public void Add( int addedValue)
    {
        _value += addedValue;
    }

    //�������� �õ� TryGet();
    public bool TryBuy(int value)
    {
        if(_value < value)
        {
            return false; //�����
        }

        _value -= value; //���
        return true;     //����
    }
}
