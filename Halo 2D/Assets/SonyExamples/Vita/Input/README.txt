Unity Vita Input Example.

Demonstrates Vita use of buttons, front and back touch and motion APIs.

NOTE: Before building the example un-zip the file InputManager.zip into the project settings folder, this is needed to setup the correct axis mappings for the vita controller.

IMPORTANT: SCE Technical Requirement TRC R3173: "The center value dead zone +-0x20 data of the left stick
 and right stick of the PlayStation Vita system is not used in the application.
"

This equates to a Unity dead zone value of 0.25 so in order to comply with this requirement you
 should set 0.25 as the dead values for each stick axis in the Unity input manager.
