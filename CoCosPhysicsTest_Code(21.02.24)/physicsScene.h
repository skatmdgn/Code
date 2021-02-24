#pragma once
#include <cocos2d.h>
USING_NS_CC;

#include <Box2D/Box2D.h>
#define PTM_RATIO 32.0
//32픽셀을 1미터로 삼겠다(픽셀과 미터간의 환산비율)

class physicsScene:public Scene
{
public:
	bool init();
	CREATE_FUNC(physicsScene);

	b2World* _world;
	//물리법칙이 적용되는 영역을 만들어 저장할 변수

	bool touchBegan(Touch* t, Event* e);
	//화면을 터치하면 물리 객체를 만들어줌

	void tick(float dt);
};

