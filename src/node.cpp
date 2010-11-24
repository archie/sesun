#include "node.h"
#include<iostream>
using namespace std;
Node::Node(){
	for(int i=0;i<SWARM_MAX;i++){
		table.mem_swarm_list[i]=NULL;
		table.mem_next_list[i]=NULL;
		}
	}

void Node::initialise (int  swarm_id, int num_nodes_swarm, int local_id){
	mem_global_id = swarm_id;
	mem_local_id = local_id;
	mem_num_nodes_swarm = num_nodes_swarm;
	}
	
	
