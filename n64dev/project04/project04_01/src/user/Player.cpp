#include "script/userScript.h"
#include "scene/sceneManager.h"

namespace P64::Script::C11955FF25B55736
{
  P64_DATA(
    // Put your arguments here if needed, those will show up in the editor.
    //
    // Types that can be set in the editor:
    // - uint8_t, int8_t, uint16_t, int16_t, uint32_t, int32_t
    // - float
    // - AssetRef<sprite_t>
    // - ObjectRef
  [[P64::Name("Velocity X")]]
  float velX;

  fm_vec3_t pos;

  float fShootCooldown = 0.0f;

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
    auto held = joypad_get_buttons_held(JOYPAD_PORT_1);
    auto joypad = joypad_get_inputs(JOYPAD_PORT_1);

    float fSpeed = 16.0f;

    constexpr fm_vec3_t dir_h{1.0f, 0.0f, 0.0f};
    obj.pos += dir_h * (float) joypad.stick_x * (deltaTime * fSpeed);


    constexpr fm_vec3_t dir_v{0.0f, 0.0f, -1.0f};
    obj.pos += dir_v * (float) joypad.stick_y * (deltaTime * fSpeed);


    if (data->fShootCooldown > 0.0f) {
      data->fShootCooldown -= deltaTime;
    }
    
    /*
    if (joypad.btn.a && data->fShootCooldown <= 0.0f) {
      data->fShootCooldown = 1.0f;
      constexpr fm_vec3_t dirZ{0.0f, 0.0f, 1.0f};
      obj.pos += dirZ *  (deltaTime * 640.0f);

    }
      */

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
