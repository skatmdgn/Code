#pragma once
#include <cocos2d.h>
USING_NS_CC;

#include <Box2D/Box2D.h>
#define PTM_RATIO 32.0
//32�ȼ��� 1���ͷ� ��ڴ�(�ȼ��� ���Ͱ��� ȯ�����)

class physicsScene:public Scene
{
public:
	bool init();
	CREATE_FUNC(physicsScene);

	b2World* _world;
	//������Ģ�� ����Ǵ� ������ ����� ������ ����

	bool touchBegan(Touch* t, Event* e);
	//ȭ���� ��ġ�ϸ� ���� ��ü�� �������

	void tick(float dt);
};

