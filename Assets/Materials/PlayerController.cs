using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Transform greenTarget;
    public Transform blueTarget;
    NavMeshAgent agent;
    public GameObject Obstacle;
    public float defendThreshold; // Add this field

    void Start()
    {
        Time.fixedDeltaTime = 0.01f; // Set the fixed frame time to 0.01 seconds
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        UpdateTarget();
    }

    void UpdateTarget()
    {

      if (tag == "BlueDefender")
      {
          // Find all the "GreenAttacker" agents in the scene
          GameObject[] greenAttackers = GameObject.FindGameObjectsWithTag("GreenAttacker");

          // Set the initial minimum distance to a large value
          float minDistance = float.MaxValue;

          // Set the initial target to the current position of the agent
          Vector3 target = transform.position;

          // Iterate through all the "GreenAttacker" agents
          foreach (GameObject greenAttacker in greenAttackers)
          {
              // Calculate the distance between the "BlueDefender" agent and the current "GreenAttacker" agent
              float distance = Vector3.Distance(transform.position, greenAttacker.transform.position);

              // If the distance is smaller than the current minimum distance
              if (distance < minDistance)
              {
                  // Set the new minimum distance
                  minDistance = distance;

                  // Set the new target to the position of the "GreenAttacker" agent
                  target = greenAttacker.transform.position;
              }
          }

          // Check if the distance to the nearest enemy is greater than the threshold
          if (minDistance > defendThreshold)
          {
              // Find all the "BlueGoalLine" objects in the scene
              GameObject[] blueGoalLines = GameObject.FindGameObjectsWithTag("BlueGoalLine");

              // Set the initial minimum distance to a large value
              minDistance = float.MaxValue;

              // Iterate through all the "BlueGoalLine" objects
              foreach (GameObject blueGoalLine in blueGoalLines)
              {
                  // Calculate the distance between the "BlueDefender" agent and the current "BlueGoalLine" object
                  float distance = Vector3.Distance(transform.position, blueGoalLine.transform.position);

                  // If the distance is smaller than the current minimum distance
                  if (distance < minDistance)
                  {
                      // Set the new minimum distance
                      minDistance = distance;

                      // Set the new target to the position of the "BlueGoalLine" object
                      target = blueGoalLine.transform.position;
                  }
              }
          }

          // Set the target of the "BlueDefender" agent to the position of the nearest "GreenAttacker" agent or the nearest "BlueGoalLine" object
          agent.SetDestination(target);
      }

    else if (tag == "GreenDefender")
    {
        // Find all the "GreenAttacker" agents in the scene
        GameObject[] blueAttackers = GameObject.FindGameObjectsWithTag("BlueAttacker");

        // Set the initial minimum distance to a large value
        float minDistance = float.MaxValue;

        // Set the initial target to the current position of the agent
        Vector3 target = transform.position;

        // Iterate through all the "GreenAttacker" agents
        foreach (GameObject blueAttacker in blueAttackers)
        {
            // Calculate the distance between the "BlueDefender" agent and the current "GreenAttacker" agent
            float distance = Vector3.Distance(transform.position, blueAttacker.transform.position);

            // If the distance is smaller than the current minimum distance
            if (distance < minDistance)
            {
                // Set the new minimum distance
                minDistance = distance;

                // Set the new target to the position of the "GreenAttacker" agent
                target = blueAttacker.transform.position;
            }
        }

        // Check if the distance to the nearest enemy is greater than the threshold
        if (minDistance > defendThreshold)
        {
            // Find all the "BlueGoalLine" objects in the scene
            GameObject[] greenGoalLines = GameObject.FindGameObjectsWithTag("GreenGoalLine");

            // Set the initial minimum distance to a large value
            minDistance = float.MaxValue;

            // Iterate through all the "BlueGoalLine" objects
            foreach (GameObject greenGoalLine in greenGoalLines)
            {
                // Calculate the distance between the "BlueDefender" agent and the current "BlueGoalLine" object
                float distance = Vector3.Distance(transform.position, greenGoalLine.transform.position);

                // If the distance is smaller than the current minimum distance
                if (distance < minDistance)
                {
                    // Set the new minimum distance
                    minDistance = distance;

                    // Set the new target to the position of the "BlueGoalLine" object
                    target = greenGoalLine.transform.position;
                }
            }
        }

        // Set the target of the "BlueDefender" agent to the position of the nearest "GreenAttacker" agent or the nearest "BlueGoalLine" object
        agent.SetDestination(target);
    }




        else if (tag == "GreenAttacker")
    {
        // Find all the "BlueGoalLine" objects in the scene
        GameObject[] blueGoalLines = GameObject.FindGameObjectsWithTag("BlueGoalLine");

        // Set the initial minimum distance to a large value
        float minDistance = float.MaxValue;

        // Set the initial target to the current position of the agent
        Vector3 target = transform.position;

        // Iterate through all the "BlueGoalLine" objects
        foreach (GameObject blueGoalLine in blueGoalLines)
        {
            // Calculate the distance between the GreenAttacker and the current BlueGoalLine
            float distance = Vector3.Distance(transform.position, blueGoalLine.transform.position);

            // If the distance is smaller than the current minimum distance
            if (distance < minDistance)
            {
                // Set the new minimum distance
                minDistance = distance;

                // Set the new target to the position of the BlueGoalLine
                target = blueGoalLine.transform.position;
            }
        }

        // Find all the "BlueDefender" agents in the scene
        GameObject[] blueDefenders = GameObject.FindGameObjectsWithTag("BlueDefender");

        // Iterate through all the "BlueDefender" agents
        foreach (GameObject blueDefender in blueDefenders)
        {
            // Calculate the distance between the GreenAttacker and the current BlueDefender
            float distance = Vector3.Distance(transform.position, blueDefender.transform.position);

            // If the distance is less than a certain threshold
            if (distance < 30.0f)
            {
                // Check if there is an obstacle (in this case, the BlueDefender) in the agent's path
                NavMeshHit hit;
                bool hasHit = agent.Raycast(target, out hit);
                if (hasHit)
                {
                    // There is an obstacle in the agent's path, so calculate a new path around it
                    NavMeshPath path = new NavMeshPath();
                    agent.CalculatePath(hit.position, path);

                    // Set the agent's destination to the new path
                    agent.SetDestination(path.corners[path.corners.Length - 1]);
                }
            }
        }

        // Check if there is an obstacle in the agent's path (in this case, any obstacle other than a BlueDefender)
        NavMeshHit hit2;
        bool hasHit2 = agent.Raycast(target, out hit2);
        if (hasHit2)
        {
            // There is an obstacle in the agent's path, so calculate a new path around it
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(hit2.position, path);

            // Set the agent's destination to the new path
            agent.SetDestination(path.corners[path.corners.Length - 1]);
        }
}

    else if (tag == "BlueAttacker")
    {
        // Find all the "BlueGoalLine" objects in the scene
        GameObject[] greenGoalLines = GameObject.FindGameObjectsWithTag("GreenGoalLine");

        // Set the initial minimum distance to a large value
        float minDistance = float.MaxValue;

        // Set the initial target to the current position of the agent
        Vector3 target = transform.position;

        // Iterate through all the "BlueGoalLine" objects
        foreach (GameObject greenGoalLine in greenGoalLines)
        {
          // Calculate the distance between the GreenAttacker and the current BlueGoalLine
          float distance = Vector3.Distance(transform.position, greenGoalLine.transform.position);

          // If the distance is smaller than the current minimum distance
          if (distance < minDistance)
          {
            // Set the new minimum distance
            minDistance = distance;

            // Set the new target to the position of the BlueGoalLine
            target = greenGoalLine.transform.position;
          }
        }

        // Find all the "BlueDefender" agents in the scene
        GameObject[] greenDefenders = GameObject.FindGameObjectsWithTag("GreenDefender");

        // Iterate through all the "BlueDefender" agents
        foreach (GameObject greenDefender in greenDefenders)
        {
          // Calculate the distance between the GreenAttacker and the current BlueDefender
          float distance = Vector3.Distance(transform.position, greenDefender.transform.position);

          // If the distance is less than a certain threshold
          if (distance < 30.0f)
          {
            // Check if there is an obstacle (in this case, the BlueDefender) in the agent's path
            NavMeshHit hit;
            bool hasHit = agent.Raycast(target, out hit);
            if (hasHit)
            {
              // There is an obstacle in the agent's path, so calculate a new path around it
              NavMeshPath path = new NavMeshPath();
              agent.CalculatePath(hit.position, path);

              // Set the agent's destination to the new path
              agent.SetDestination(path.corners[path.corners.Length - 1]);
            }
          }
        }

        // Check if there is an obstacle in the agent's path (in this case, any obstacle other than a BlueDefender)
        NavMeshHit hit2;
        bool hasHit2 = agent.Raycast(target, out hit2);
        if (hasHit2)
        {
          // There is an obstacle in the agent's path, so calculate a new path around it
          NavMeshPath path = new NavMeshPath();
          agent.CalculatePath(hit2.position, path);

          // Set the agent's destination to the new path
          agent.SetDestination(path.corners[path.corners.Length - 1]);
        }
      }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (tag == "BlueAttacker" && collision.gameObject.tag == "GreenGoalLine")
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            tag = "GreenAttacker";
            KeepScore.BlueScore += 1;

            // Drop the object at a random place within the given area
            float x = Random.Range(Obstacle.transform.position.x + 30, Obstacle.transform.position.x + 45);
            float y = Obstacle.transform.position.y;
            float z = Random.Range(Obstacle.transform.position.z - 45, Obstacle.transform.position.z + 45);
            Vector3 randomPosition = new Vector3(x, y, z);

            float randomY = Random.Range(0, 360);

            Quaternion randomQuaternion = Quaternion.Euler(0, randomY, 0);

            Instantiate(Obstacle, randomPosition, randomQuaternion);
        }
        else if (tag == "GreenAttacker" && collision.gameObject.tag == "BlueGoalLine")
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
            tag = "BlueAttacker";
            KeepScore.GreenScore += 1;

            // Drop the object at a random place within the given area
            float x = Random.Range(Obstacle.transform.position.x - 30, Obstacle.transform.position.x - 45);
            float y = Obstacle.transform.position.y;
            float z = Random.Range(Obstacle.transform.position.z - 45, Obstacle.transform.position.z + 45);
            Vector3 randomPosition = new Vector3(x, y, z);

            float randomY = Random.Range(0, 360);

            Quaternion randomQuaternion = Quaternion.Euler(0, randomY, 0);

            Instantiate(Obstacle, randomPosition, randomQuaternion);

          }
          else if (tag == "GreenAttacker" && collision.gameObject.tag == "BlueDefender")
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
            tag = "BlueAttacker";
        }
        else if (tag == "BlueAttacker" && collision.gameObject.tag == "GreenDefender")
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            tag = "GreenAttacker";
        }

}
}
