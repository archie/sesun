#define SWARM_MAX 10
class Node;
struct Fingertable{
	Node *mem_swarm_list[SWARM_MAX];
	Node *mem_next_list[SWARM_MAX];
	//int keys;
	};
	
class Node{
	public:
	int mem_global_id;
	int mem_local_id;
	int mem_num_nodes_swarm;
	Fingertable table;
	
	public:
	Node();
	void initialise(int swarm_id, int num_nodes_swarm, int local_id);
	};
	