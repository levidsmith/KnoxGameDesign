#include "script/userScript.h"
#include "scene/sceneManager.h"

namespace P64::Script::CD35997A5611024E
{
  P64_DATA(
    [[P64::Name("Translate Speed")]]
    float translateSpeed;

    [[P64::Name("Rotate Speed")]]
    float rotateSpeed;

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
    constexpr fm_vec3_t rotAxis{0.0f, 1.0f, 0.0f};
    fm_quat_rotate(&obj.rot, &obj.rot, &rotAxis, deltaTime * data->rotateSpeed);
    fm_quat_norm(&obj.rot, &obj.rot);

    constexpr fm_vec3_t dir{0.0f, 1.0f, 0.0f};
    obj.pos += dir * (deltaTime * data->translateSpeed);

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
