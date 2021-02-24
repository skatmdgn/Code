#include "physicsScene.h"

bool physicsScene::init()
{
    Size winSize = Director::getInstance()->getWinSize();
    b2Vec2 gravity = b2Vec2(0.0, -30.0);//중력값 생성

    _world = new b2World(gravity);
    //물리영역을 만들고 중력값으로 gravity 사용

    _world->SetAllowSleeping(true);//게임이 대기중일때도 물리연산을 할 것인지
    _world->SetContinuousPhysics(true);//지속적인 물리연산을 적용할 것인지

    b2BodyDef groundBodyDef;
    groundBodyDef.position.Set(0, 0);

    //월드에 바디데프의 정보(좌표)로 바디를 만듬
    b2Body* groundBody = _world->CreateBody(&groundBodyDef);
    
    //월드 바깥으로 빠져나가지 못하도록 4개의 면에 테두리를 침
    b2EdgeShape groundEdge; //바디의 형태(모양)를 정의
    b2FixtureDef boxShapeDef; //실제 바디를 정의
    boxShapeDef.shape = &groundEdge; //바디에 형태를 적용

    //아래쪽
    groundEdge.Set(b2Vec2(0, 0), b2Vec2(winSize.width / PTM_RATIO, 0));
    groundBody->CreateFixture(&boxShapeDef);

    //왼쪽
    groundEdge.Set(b2Vec2(0, 0), b2Vec2(0, winSize.height / PTM_RATIO));
    groundBody->CreateFixture(&boxShapeDef);

    //위쪽
    groundEdge.Set(b2Vec2(0, winSize.height / PTM_RATIO), b2Vec2(winSize.width / PTM_RATIO, winSize.height / PTM_RATIO));
    groundBody->CreateFixture(&boxShapeDef);

    //오른쪽
    groundEdge.Set(b2Vec2(winSize.width / PTM_RATIO, winSize.height / PTM_RATIO), b2Vec2(winSize.width / PTM_RATIO, 0));
    groundBody->CreateFixture(&boxShapeDef);

    EventListenerTouchOneByOne* t = EventListenerTouchOneByOne::create();
    t->onTouchBegan = CC_CALLBACK_2(physicsScene::touchBegan, this);
    _eventDispatcher->addEventListenerWithSceneGraphPriority(t, this);

    this->schedule(schedule_selector(physicsScene::tick), 0.01f);
    //업데이트처럼 사용하되 업데이트 주기를 사용자가 임의로 정할 수 있음

    {
        Sprite* spr = Sprite::create("UI/buttonLarge.png");
        this->addChild(spr);
        spr->setPosition(Vec2(300, 200));

        b2BodyDef bodyDef; //물리연산 적용되는 몸통 만들기
        bodyDef.type = b2_staticBody;
        /*몸통을 동적강체(서로 물리연산하는 바디)로 만듬, 반대로 물리연산은 해도
        자신은 움직이지 않는 (장애물이나 벽) 강체는 b2_staticBody로 만듬*/
        bodyDef.position = b2Vec2(300 / PTM_RATIO, 200 / PTM_RATIO);
        /*물리객체의 위치 지정, 픽셀 좌표계가 아닌 물리 좌표계를 써야하기 때문에
        기존 좌표에 PTM_RATIO만큼 나눠서 비율을 맞춤*/

        bodyDef.userData = spr;
        /*강체에 스프라이트 정보를 입력함, 이후 강체가 움직였을 때 강체와 쌍이 되는
        스프라이트도 동일하게 움직임을 적용하기 위해서임 다만 데이터만 가지고 있지
        서로 부모 자식 관계가 아니기에 스프라이트의 위치가 자동으로 변경되진 않음*/

        b2Body* body = _world->CreateBody(&bodyDef);
        //위에서 작성한 바디의 정보를 토대로 실제 바디를 월드 내에 생성

        b2PolygonShape circle;
        circle.SetAsBox(196 * 0.5 / PTM_RATIO, 70 * 0.5 / PTM_RATIO);

        b2FixtureDef fixtureDef; //바다의 형태를 정의하는 객체
        fixtureDef.shape = &circle; //모양을 위에서 정의한 원형으로

        //밀도
        fixtureDef.density = 1.0f;

        //마찰력 - 0~1
        fixtureDef.friction = 0.2f;

        //반발력 - 물체가 다른 물체에 닿았을 때 팅기는 값
        fixtureDef.restitution = 0.7f;

        body->CreateFixture(&fixtureDef);
    }

    return true;
}

bool physicsScene::touchBegan(Touch* t, Event* e)
{
    Sprite* spr = Sprite::create("parrot.png");
    this->addChild(spr);
    spr->setScale(0.2);
    spr->setPosition(t->getLocation());

    b2BodyDef bodyDef; //물리연산 적용되는 몸통 만들기
    bodyDef.type = b2_dynamicBody;
    /*몸통을 동적강체(서로 물리연산하는 바디)로 만듬, 반대로 물리연산은 해도
    자신은 움직이지 않는 (장애물이나 벽) 강체는 b2_staticBody로 만듬*/
    bodyDef.position = b2Vec2(t->getLocation().x / PTM_RATIO, t->getLocation().y / PTM_RATIO);
    /*물리객체의 위치 지정, 픽셀 좌표계가 아닌 물리 좌표계를 써야하기 때문에
    기존 좌표에 PTM_RATIO만큼 나눠서 비율을 맞춤*/

    bodyDef.userData = spr;
    /*강체에 스프라이트 정보를 입력함, 이후 강체가 움직였을 때 강체와 쌍이 되는
    스프라이트도 동일하게 움직임을 적용하기 위해서임 다만 데이터만 가지고 있지
    서로 부모 자식 관계가 아니기에 스프라이트의 위치가 자동으로 변경되진 않음*/

    b2Body* body = _world->CreateBody(&bodyDef);
    //위에서 작성한 바디의 정보를 토대로 실제 바디를 월드 내에 생성

    b2CircleShape circle;
    circle.m_radius = spr->getContentSize().width * spr->getScale() / 2 / PTM_RATIO;
    //스프라이트의 가로폭*스케일/2(반지름이니)/PTM_RATIO(물리좌표계)

    b2FixtureDef fixtureDef; //바다의 형태를 정의하는 객체
    fixtureDef.shape = &circle; //모양을 위에서 정의한 원형으로

    //밀도
    fixtureDef.density = 1.0f;

    //마찰력 - 0~1
    fixtureDef.friction = 0.2f;

    //반발력 - 물체가 다른 물체에 닿았을 때 팅기는 값
    fixtureDef.restitution = 0.7f;

    body->CreateFixture(&fixtureDef);

    return true;
}

void physicsScene::tick(float dt)
{
    int vel = 8;
    int pos = 3;

    _world->Step(dt, vel, pos);

    for (b2Body* b = _world->GetBodyList(); b; b = b->GetNext())
    {
        Sprite* sprData = (Sprite*)b->GetUserData();
        if (sprData != NULL)
        {
            sprData->setPosition(Vec2(b->GetPosition().x * PTM_RATIO, b->GetPosition().y * PTM_RATIO));
            sprData->setRotation(-1 * CC_RADIANS_TO_DEGREES(b->GetAngle()));
        }
    }
}
