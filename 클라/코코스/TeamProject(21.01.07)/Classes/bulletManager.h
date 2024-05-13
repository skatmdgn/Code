#pragma once
#include <cocos2d.h>
USING_NS_CC;
class bulletManager:public Node
{
public:
	bool init();
	
	Vector<Node*> b_e_vec;
	Vector<Node*> b_e_vec_wait;

	Vector<Node*> b_p_vec1;
	Vector<Node*> b_p_vec1_wait;

	Vector<Node*> b_p_vec2;
	Vector<Node*> b_p_vec2_wait;

	void update(float dt);

	void reset();

	static bulletManager* getIns();
private:
	CREATE_FUNC(bulletManager);
};

