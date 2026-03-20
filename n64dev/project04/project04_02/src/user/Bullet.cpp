#include "script/userScript.h"
#include "scene/sceneManager.h"

namespace P64::Script::CF71A67F563001DA
{
  P64_DATA(
    // Put your arguments here if needed, those will show up in the editor.
    //
    // Types that can be set in the editor:
    // - uint8_t, int8_t, uint16_t, int16_t, uint32_t, int32_t
    // - float
    // - AssetRef<sprite_t>
    // - ObjectRef
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
        auto joypad = joypad_get_inputs(JOYPAD_PORT_1);

    constexpr fm_vec3_t dir{0.0f, 0.0f, -1.0f};
    if (obj.pos.z > -1200.0f) {    
      obj.pos += dir *  (deltaTime * 64.0f * 20.0f);
    }

    if (joypad.btn.a) {
      constexpr fm_vec3_t pos{0.0f, 0.0f, 428.0f};
      obj.pos = pos;

    }

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
