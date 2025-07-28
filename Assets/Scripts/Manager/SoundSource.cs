using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource; // �Ҹ��� ����� AudioSource ������Ʈ

    // ȿ������ ����ϴ� �Լ�
    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        // AudioSource�� ��� ������ �������� (���� �� ��)
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        CancelInvoke(); // ���� Invoke ������ �ִٸ� ���
        _audioSource.clip = clip; ; // ����� ����� Ŭ�� ����
        _audioSource.volume = soundEffectVolume; // ���� ����
        _audioSource.Play(); // ���� ���

        // ���� ���� + ���� �ð� ���Ŀ� �ڵ� ����
        Invoke("Disable", clip.length + 2);
    }

    public void Disable()
    {
        _audioSource.Stop();
        Destroy(this.gameObject);
    }
}