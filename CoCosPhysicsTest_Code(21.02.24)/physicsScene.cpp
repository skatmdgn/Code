#include "physicsScene.h"

bool physicsScene::init()
{
    Size winSize = Director::getInstance()->getWinSize();
    b2Vec2 gravity = b2Vec2(0.0, -30.0);//�߷°� ����

    _world = new b2World(gravity);
    //���������� ����� �߷°����� gravity ���

    _world->SetAllowSleeping(true);//������ ������϶��� ���������� �� ������
    _world->SetContinuousPhysics(true);//�������� ���������� ������ ������

    b2BodyDef groundBodyDef;
    groundBodyDef.position.Set(0, 0);

    //���忡 �ٵ����� ����(��ǥ)�� �ٵ� ����
    b2Body* groundBody = _world->CreateBody(&groundBodyDef);
    
    //���� �ٱ����� ���������� ���ϵ��� 4���� �鿡 �׵θ��� ħ
    b2EdgeShape groundEdge; //�ٵ��� ����(���)�� ����
    b2FixtureDef boxShapeDef; //���� �ٵ� ����
    boxShapeDef.shape = &groundEdge; //�ٵ� ���¸� ����

    //�Ʒ���
    groundEdge.Set(b2Vec2(0, 0), b2Vec2(winSize.width / PTM_RATIO, 0));
    groundBody->CreateFixture(&boxShapeDef);

    //����
    groundEdge.Set(b2Vec2(0, 0), b2Vec2(0, winSize.height / PTM_RATIO));
    groundBody->CreateFixture(&boxShapeDef);

    //����
    groundEdge.Set(b2Vec2(0, winSize.height / PTM_RATIO), b2Vec2(winSize.width / PTM_RATIO, winSize.height / PTM_RATIO));
    groundBody->CreateFixture(&boxShapeDef);

    //������
    groundEdge.Set(b2Vec2(winSize.width / PTM_RATIO, winSize.height / PTM_RATIO), b2Vec2(winSize.width / PTM_RATIO, 0));
    groundBody->CreateFixture(&boxShapeDef);

    EventListenerTouchOneByOne* t = EventListenerTouchOneByOne::create();
    t->onTouchBegan = CC_CALLBACK_2(physicsScene::touchBegan, this);
    _eventDispatcher->addEventListenerWithSceneGraphPriority(t, this);

    this->schedule(schedule_selector(physicsScene::tick), 0.01f);
    //������Ʈó�� ����ϵ� ������Ʈ �ֱ⸦ ����ڰ� ���Ƿ� ���� �� ����

    {
        Sprite* spr = Sprite::create("UI/buttonLarge.png");
        this->addChild(spr);
        spr->setPosition(Vec2(300, 200));

        b2BodyDef bodyDef; //�������� ����Ǵ� ���� �����
        bodyDef.type = b2_staticBody;
        /*������ ������ü(���� ���������ϴ� �ٵ�)�� ����, �ݴ�� ���������� �ص�
        �ڽ��� �������� �ʴ� (��ֹ��̳� ��) ��ü�� b2_staticBody�� ����*/
        bodyDef.position = b2Vec2(300 / PTM_RATIO, 200 / PTM_RATIO);
        /*������ü�� ��ġ ����, �ȼ� ��ǥ�谡 �ƴ� ���� ��ǥ�踦 ����ϱ� ������
        ���� ��ǥ�� PTM_RATIO��ŭ ������ ������ ����*/

        bodyDef.userData = spr;
        /*��ü�� ��������Ʈ ������ �Է���, ���� ��ü�� �������� �� ��ü�� ���� �Ǵ�
        ��������Ʈ�� �����ϰ� �������� �����ϱ� ���ؼ��� �ٸ� �����͸� ������ ����
        ���� �θ� �ڽ� ���谡 �ƴϱ⿡ ��������Ʈ�� ��ġ�� �ڵ����� ������� ����*/

        b2Body* body = _world->CreateBody(&bodyDef);
        //������ �ۼ��� �ٵ��� ������ ���� ���� �ٵ� ���� ���� ����

        b2PolygonShape circle;
        circle.SetAsBox(196 * 0.5 / PTM_RATIO, 70 * 0.5 / PTM_RATIO);

        b2FixtureDef fixtureDef; //�ٴ��� ���¸� �����ϴ� ��ü
        fixtureDef.shape = &circle; //����� ������ ������ ��������

        //�е�
        fixtureDef.density = 1.0f;

        //������ - 0~1
        fixtureDef.friction = 0.2f;

        //�ݹ߷� - ��ü�� �ٸ� ��ü�� ����� �� �ñ�� ��
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

    b2BodyDef bodyDef; //�������� ����Ǵ� ���� �����
    bodyDef.type = b2_dynamicBody;
    /*������ ������ü(���� ���������ϴ� �ٵ�)�� ����, �ݴ�� ���������� �ص�
    �ڽ��� �������� �ʴ� (��ֹ��̳� ��) ��ü�� b2_staticBody�� ����*/
    bodyDef.position = b2Vec2(t->getLocation().x / PTM_RATIO, t->getLocation().y / PTM_RATIO);
    /*������ü�� ��ġ ����, �ȼ� ��ǥ�谡 �ƴ� ���� ��ǥ�踦 ����ϱ� ������
    ���� ��ǥ�� PTM_RATIO��ŭ ������ ������ ����*/

    bodyDef.userData = spr;
    /*��ü�� ��������Ʈ ������ �Է���, ���� ��ü�� �������� �� ��ü�� ���� �Ǵ�
    ��������Ʈ�� �����ϰ� �������� �����ϱ� ���ؼ��� �ٸ� �����͸� ������ ����
    ���� �θ� �ڽ� ���谡 �ƴϱ⿡ ��������Ʈ�� ��ġ�� �ڵ����� ������� ����*/

    b2Body* body = _world->CreateBody(&bodyDef);
    //������ �ۼ��� �ٵ��� ������ ���� ���� �ٵ� ���� ���� ����

    b2CircleShape circle;
    circle.m_radius = spr->getContentSize().width * spr->getScale() / 2 / PTM_RATIO;
    //��������Ʈ�� ������*������/2(�������̴�)/PTM_RATIO(������ǥ��)

    b2FixtureDef fixtureDef; //�ٴ��� ���¸� �����ϴ� ��ü
    fixtureDef.shape = &circle; //����� ������ ������ ��������

    //�е�
    fixtureDef.density = 1.0f;

    //������ - 0~1
    fixtureDef.friction = 0.2f;

    //�ݹ߷� - ��ü�� �ٸ� ��ü�� ����� �� �ñ�� ��
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
