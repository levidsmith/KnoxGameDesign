#include "script/userScript.h"
#include "scene/sceneManager.h"

namespace P64::Script::C5C615CFB1AF1FC6
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
    auto held = joypad_get_buttons_held(JOYPAD_PORT_1);
    float fSpeed = 3.14f;

    constexpr fm_vec3_t rotX{0.0f, 1.0f, 0.0f};
    fm_quat_rotate(&obj.rot, &obj.rot, &rotX, deltaTime * joypad.stick_x / 128.0f * 3.14f);
    fm_quat_norm(&obj.rot, &obj.rot);

    constexpr fm_vec3_t rotY{1.0f, 0.0f, 0.0f};
    fm_quat_rotate(&obj.rot, &obj.rot, &rotY, deltaTime * joypad.stick_y / 128.0f * 3.14f);
    fm_quat_norm(&obj.rot, &obj.rot);


    if (held.l || held.r) {
      constexpr fm_vec3_t rotAxis{0.0f, 0.0f, 1.0f};
      
      if (held.l) {
        fSpeed = 3.14f;
      } else if (held.r) {
        fSpeed = -3.14f;
      }

      fm_quat_rotate(&obj.rot, &obj.rot, &rotAxis, deltaTime * fSpeed);
      fm_quat_norm(&obj.rot, &obj.rot);

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
