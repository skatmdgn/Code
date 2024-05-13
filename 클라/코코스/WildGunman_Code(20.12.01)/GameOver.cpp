#include "GameOver.h"
#include "MainScene.h"

bool GameOver::init()
{
    Label* ti = Label::createWithTTF("GAME\nOVER" , "fonts/pixel-nes.otf", 70);
    this->addChild(ti);
    ti->setPosition(Vec2(256, 240));

    this->scheduleUpdate();

    return true;
}

void GameOver::update(float dt)
{
    timer += dt;
    if (timer >= 7)
    {
        MainScene* ms = MainScene::create();
        Director::getInstance()->replaceScene(ms);
    }
}
