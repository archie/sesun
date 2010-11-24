#include<iostream>
#include<fstream>
using namespace std;
#include "node.h"

struct position{
	Node *head;
	Node *previous;
	};
	
void setup_swarm (Node *node, int num_nodes);
position setup_chord (position pos, Node *node, int num_nodes);
void show_swarm (Node *node, int num_nodes);
void show_next_list (Node *node);

int main(){
	int temp, max_count, num_nodes_swarm, swarm_id, nodes_initialised=0;
	Node *node;
	
	position pos;
	pos.head=NULL, pos.previous=NULL;
	
	fstream file ("input.conf");
	file>> max_count;
	cout<< "Total number of nodes: "<< max_count;
	node = new Node [max_count];

	while (!file.eof()){
		file>> num_nodes_swarm;
		file>> swarm_id;
		cout<< "\n Num. of nodes in swarm: "<<num_nodes_swarm<<"Swarm id:  "<< swarm_id;
		for(int i=0;i<num_nodes_swarm;i++){
			 node[i+nodes_initialised].initialise(swarm_id, num_nodes_swarm, i);
			}
			
		setup_swarm (node+nodes_initialised, num_nodes_swarm);
		show_swarm (node+nodes_initialised, num_nodes_swarm);
		pos = setup_chord ( pos, node+nodes_initialised, num_nodes_swarm );
		//show_next_list (node+nodes_initialised);
		nodes_initialised+=num_nodes_swarm;
		}
	
	return 0;
	}
	

void setup_swarm( Node *node, int num_nodes){
		int k;
		for(int i=0; i<num_nodes; i++){
			k=0;
			for (int j=0; j<num_nodes; j++){
				if(i!=j){
					node[i].table.mem_swarm_list[k]= &node[j]; 
					k++;
					}
				}
			}
		}
		
position setup_chord ( position pos, Node *node, int num_nodes ){
	if (pos.head == NULL && pos.previous == NULL){
		pos.head= pos.previous = node;
		int k ;
		for(int i=0; i<num_nodes; i++){
			k=0;
			for (int j=0; j<num_nodes; j++){
				if(i!=j){
				//	cout<< "\n Entry made for node "<< i <<" "<< k <<"\n ";
					node[i].table.mem_next_list[k]= &node[j]; 
				//	cout<< node[i].table.mem_next_list[k]->mem_local_id;
					k++;
					}
				}
			}
			return pos;
		}
	
	if (pos.head!=NULL && pos.previous!=NULL) {
		for (int i=0;i< (pos.previous->mem_num_nodes_swarm-1);i++){
			for (int j=0; j<num_nodes; j++){
				pos.previous->table.mem_next_list[j]=&node[j];
				}
			pos.previous = pos.previous->table.mem_swarm_list[i];
			}
		for (int i=0; i<(pos.head->mem_num_nodes_swarm-1); i++){
			for (int j=0; j<num_nodes; j++){
					node[j].table.mem_next_list[i]= pos.head;	
				}
			pos.head= pos.head->table.mem_swarm_list[i];
			}
			return pos;
		}
	}
	
void show_swarm( Node *node, int num_nodes){
		int k;
		cout<< "\nSwarm list for each Node: \n";
		for(int i=0; i<num_nodes; i++){
			cout<< "\n";
			cout<<"  Node "<<i;
			k=0;
			for (int j=0; j<num_nodes; j++){
				if(i!=j){
					cout<</*" Global_id: "<< node[i].table.mem_swarm_list[j]->mem_global_id<< */" Local_id: "<<node[i].table.mem_swarm_list[k]->mem_local_id; ; 
					k++;
					}
				}
			}
		}
		
void show_next_list (Node *node){
	int i=0;
	while(node->table.mem_next_list[i]!=NULL){
		cout<< "\n Next List: " << node->table.mem_next_list[i]->mem_global_id; 
		i++;
		}
	}