#include "script/userScript.h"
#include "scene/sceneManager.h"

namespace P64::Script::C4290FC27115D2FD
{
  P64_DATA(
    // Put your arguments here if needed, those will show up in the editor.
    //
    // Types that can be set in the editor:
    // - uint8_t, int8_t, uint16_t, int16_t, uint32_t, int32_t
    // - float
    // - AssetRef<sprite_t>
    // - ObjectRef
    [[P64::Name("Countdown")]]
    float fCountdown = 1.0f;

    [[P64::Name("Max Countdown")]]
    float fMaxCountdown = 2.0f;

    [[P64::Name("Speed")]]
    float fSpeed = 64.0f * 5.0f;

    int noExposed;

  );

  // The following functions are called by the engine at different points in the object's lifecycle.
  // If you don't need a specific function you can remove it.

  void initDelete(Object& obj, Data *data, bool isDelete)
  {
    if(isDelete) {
      // do cleanup
      return;
    }
    // do initialization
  }

  void update(Object& obj, Data *data, float deltaTime) {
    data->fCountdown -= deltaTime;
    if (data->fCountdown <= 0.0f) {
      data->fCountdown = data->fMaxCountdown;
      data->fSpeed *= -1.0f;
    }

    constexpr fm_vec3_t vel{1.0f, 0.0f, 0.0f};
    obj.pos += vel * (deltaTime * data->fSpeed);
  }

  void draw(Object& obj, Data *data, float deltaTime)
  {
  }

  void onEvent(Object& obj, Data *data, const ObjectEvent &event)
  {
  }

  void onCollision(Object& obj, Data *data, const Coll::CollEvent& event)
  {
  }
}
