using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdAnimation : MonoBehaviour
{
    private Animator m_Animator;
    private UnityEngine.AI.NavMeshAgent agent;


    IEnumerator Start()
    {
        m_Animator = transform.Find("body")?.GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        

        agent.autoTraverseOffMeshLink = false;

        while (true)
        {
            if (agent.isOnOffMeshLink == true)
            {
                yield return StartCoroutine(Parabola(agent, 2.0f, 1f));
                agent.CompleteOffMeshLink();
            }
            yield return null;
        }


    }

    void Update()
    {
        if (agent.isOnOffMeshLink == false)
        {
            m_Animator.SetBool("isJumping", false);
        }
    }


    IEnumerator Parabola(UnityEngine.AI.NavMeshAgent agent, float height, float duration)
    {
        UnityEngine.AI.OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            m_Animator.SetBool("isJumping", true);

            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        
    }

}
