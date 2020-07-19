// GENERATED AUTOMATICALLY FROM 'Assets/OldBuildMisc/Hyukin_Kwons/InputAction/PlayerInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""47558235-f427-4844-94fd-6e669c4852a2"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bdce686e-f2c5-4360-88b7-c19d533ebfa3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""27ba75bd-e10e-4f74-881a-d5c14b053f4b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActivateHook"",
                    ""type"": ""Button"",
                    ""id"": ""6298dd73-24c4-47ca-89f2-34b42bf65d0b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ElecArm"",
                    ""type"": ""Button"",
                    ""id"": ""56a0e44a-df62-43f1-b37f-028d909a1dc5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActivateMagArm"",
                    ""type"": ""Button"",
                    ""id"": ""27dca19b-aede-49a3-a087-caa37659cb1f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""7811d3be-02ce-4c56-be3a-53df1b884565"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""MoveAxis"",
                    ""id"": ""2d998636-e2fc-4953-b329-7859f087a4f4"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cd7dbdd5-e88b-4e55-9a48-a8c7c7c6551b"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""976edf49-59d5-409f-966b-12430845fba7"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""42bece95-25ec-4320-b386-5f42b8ce4fde"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d6e16011-0c2d-4119-afae-4c9a62f1799e"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""85024393-279d-4d7d-89e5-125dadf0d644"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""be047dff-a4cf-4131-a12f-799361720583"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7140730f-8ac9-4610-a7f5-2320f64772cf"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5167d804-1402-41d6-9c03-bbfc1b22e90a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""02942a27-5f9a-4237-8586-1784bec131ac"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a4b694ce-0d10-4c56-9a72-c038c0589e94"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2737dd3e-bc73-418b-a203-99080f6762bc"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6cc62609-36fa-4ea7-9cb8-bf43da07e1bc"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ActivateHook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""495b632a-6f12-497c-800c-bb9774eef36e"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ElecArm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b00e7355-636d-488c-85a9-1da79951eaf5"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ActivateMagArm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b24a664-9a05-412e-9d2e-4576b5cc5c4e"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5990aa3-821c-47cd-b8d0-dab59b30b7cb"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""InventoryControls"",
            ""id"": ""b07e10e3-90e4-45e2-af4e-0ca004d01b92"",
            ""actions"": [
                {
                    ""name"": ""Left Bumper"",
                    ""type"": ""Button"",
                    ""id"": ""96fcca6c-8af8-41d3-9a29-550779a22693"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right Bumper"",
                    ""type"": ""Button"",
                    ""id"": ""f59fdf44-6f47-49b8-a790-0357988b8d93"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftSelectCog"",
                    ""type"": ""Button"",
                    ""id"": ""16e84fa2-5b4f-4f6f-b652-04d56b19475e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightSelectCog"",
                    ""type"": ""Button"",
                    ""id"": ""3b2f8056-134a-4de9-a533-ccb3853960b0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""East Button"",
                    ""type"": ""Button"",
                    ""id"": ""04748b9e-1b5a-4987-9681-ecdad844cf1a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""South Button"",
                    ""type"": ""Button"",
                    ""id"": ""6861ee0b-ad32-47fb-94c7-7909cc523062"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectPushed"",
                    ""type"": ""Button"",
                    ""id"": ""524d8a71-3cab-43f4-b090-8bb9972209ad"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""897c15cc-dc3a-4189-a1b6-ddab6841ef7a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""ea9d91ea-40c6-4562-bcf9-e4c592fe0117"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""8582c7af-b2c5-4cc5-b4ef-baa79247ebba"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""647367cc-52b9-4697-bcba-a9b0fbd84242"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""df1970f9-7f88-49f2-9d60-fec4195a338d"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Left Bumper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9a3987f-9a4d-4818-a619-c60793f206cc"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Right Bumper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13a11cdc-417e-4308-a848-7a7e5ec74a9c"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LeftSelectCog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e69e55e-c8b2-4a73-ae78-bb54f91dc342"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LeftSelectCog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""905876c8-133d-4ff2-b21a-30eaed5dde01"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RightSelectCog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""81fd1348-ca24-4903-bc7b-f4116fb7a60b"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RightSelectCog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42ff141b-b233-40ca-aa16-3ea30a0e9f1c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""East Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98fcad01-a135-400a-a5ae-78f73d0d61e4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""South Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14a2bafd-1a5a-4ea8-83d2-d3a0cadfa04d"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SelectPushed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb84442a-e9fd-4b4a-942d-7c12764f3ea1"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ce5a48d-53d9-4aee-b14f-d1ba3462e121"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de0389e8-cc18-468e-b08e-a330c0a8ec52"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""abbcc1af-9dbe-48eb-a610-7ae4c80d90c5"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TimeControls"",
            ""id"": ""136283d1-fea4-4205-a5f3-2b18bdd596b6"",
            ""actions"": [
                {
                    ""name"": ""TimeSlow"",
                    ""type"": ""Button"",
                    ""id"": ""85cce74a-33c6-4679-b429-929bdede1512"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TimeFastForward"",
                    ""type"": ""Button"",
                    ""id"": ""beeb6ae6-8c00-40f0-8b70-3fe079cdb8bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TimeStop"",
                    ""type"": ""Button"",
                    ""id"": ""b79dad23-a539-4be1-80eb-8537c3c9e281"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TimeJumpForward"",
                    ""type"": ""Button"",
                    ""id"": ""f9888814-83ef-4054-965d-b884c2154ce8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8bc31d11-d30a-44be-b5d1-a504ab0f2c20"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TimeSlow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36557dd6-47ce-4843-91d5-8317f703326a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""TimeSlow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8eb9dfe0-22a8-4fbd-a8b5-93563c78ecbf"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TimeFastForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f20fba63-65c3-49c4-abbb-6e452df2296a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""TimeFastForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a97dd005-b8c3-40d6-b1cc-40df864f7758"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TimeStop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5bb4637-44d3-4346-8378-f31301d40c92"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""TimeStop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47128d54-e06e-462d-8f18-f9c91261ceba"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TimeJumpForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MenuControls"",
            ""id"": ""445bd6c3-500f-4cb5-9394-b89d77440fd5"",
            ""actions"": [
                {
                    ""name"": ""Cancel/Back"",
                    ""type"": ""Button"",
                    ""id"": ""252539c8-1ad4-4947-9811-4c918c4d4c91"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1d2f590f-b61c-410a-b3ae-75aac7ec6c89"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel/Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afd7d395-15ee-442a-8d93-e60ff9b2fc33"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel/Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0281f86b-0530-41e7-8895-381cd52245ec"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel/Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02a75711-ea0b-457d-a6a1-9f2dd85c806c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel/Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CameraDebugAngles"",
            ""id"": ""d4da769c-d12d-4ca5-9830-5f2a8b7b925c"",
            ""actions"": [
                {
                    ""name"": ""CycleAngles"",
                    ""type"": ""Button"",
                    ""id"": ""318a9c58-fc23-4f0c-94b9-7a5f6fc6d45b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeAngle"",
                    ""type"": ""PassThrough"",
                    ""id"": ""97b0d652-d26e-47b6-b731-572ce039744f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeAngleLeft"",
                    ""type"": ""Button"",
                    ""id"": ""f33c5ff6-704d-4841-8bdc-9092daaa9a19"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeAngleRight"",
                    ""type"": ""Button"",
                    ""id"": ""e42ff36b-5957-4b21-b1e0-c4c8b63bb884"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""99a588db-cc91-4b00-a3f7-0a4707b22ecc"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CycleAngles"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5873a31f-6e08-4526-8298-e5e88be45062"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeAngle"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""77293853-6fa5-488b-86db-84ed5863bf4a"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeAngle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d7766681-f502-4d98-b569-b9f3505543c6"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeAngle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ef3a28f6-6e0b-4144-84f0-6a1d1d970507"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeAngle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2ecd38aa-15e5-490f-ba2d-1e09132e943b"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeAngle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a39e2816-076b-4ba3-997a-ed586762dae8"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ChangeAngleLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4d835fd-934a-42ea-8f4d-1aa3055afddf"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ChangeAngleRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MainMenu"",
            ""id"": ""c170cb10-b5f4-4493-80e5-c7b3383a5b09"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""39dd25c4-f7c7-460e-ac88-2b522c404f3f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""662c3aaa-ec5c-4796-bb1e-be32e1f42a69"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Button"",
                    ""id"": ""94371468-080a-4eb7-baf5-895ecabcb6f0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""907c8f9d-c687-4b9e-bacb-e240d8039e59"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d9d94224-5d4f-4470-a489-6e24f4eab0e2"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32aea564-7a2a-4dd8-8738-801b913b3959"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fab55a6f-712b-4ffe-9bd3-832a893d6420"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0cf87204-8826-49f4-be91-208f87a366af"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d3ab1f4-daf6-43b6-bb70-d2cf5d636915"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e9ca9d2-df15-42cc-a7bf-173090aef64d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef2d811d-38ef-44a9-97b9-4cd66377dbfd"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ee13a4c-a58b-4736-8673-dabb6293b3b0"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e7978ad-c88a-40b9-a109-ab167209d86d"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23edbaa3-4f7b-489b-8dd3-582d19fe5784"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_Move = m_PlayerControls.FindAction("Move", throwIfNotFound: true);
        m_PlayerControls_Jump = m_PlayerControls.FindAction("Jump", throwIfNotFound: true);
        m_PlayerControls_ActivateHook = m_PlayerControls.FindAction("ActivateHook", throwIfNotFound: true);
        m_PlayerControls_ElecArm = m_PlayerControls.FindAction("ElecArm", throwIfNotFound: true);
        m_PlayerControls_ActivateMagArm = m_PlayerControls.FindAction("ActivateMagArm", throwIfNotFound: true);
        m_PlayerControls_Interact = m_PlayerControls.FindAction("Interact", throwIfNotFound: true);
        // InventoryControls
        m_InventoryControls = asset.FindActionMap("InventoryControls", throwIfNotFound: true);
        m_InventoryControls_LeftBumper = m_InventoryControls.FindAction("Left Bumper", throwIfNotFound: true);
        m_InventoryControls_RightBumper = m_InventoryControls.FindAction("Right Bumper", throwIfNotFound: true);
        m_InventoryControls_LeftSelectCog = m_InventoryControls.FindAction("LeftSelectCog", throwIfNotFound: true);
        m_InventoryControls_RightSelectCog = m_InventoryControls.FindAction("RightSelectCog", throwIfNotFound: true);
        m_InventoryControls_EastButton = m_InventoryControls.FindAction("East Button", throwIfNotFound: true);
        m_InventoryControls_SouthButton = m_InventoryControls.FindAction("South Button", throwIfNotFound: true);
        m_InventoryControls_SelectPushed = m_InventoryControls.FindAction("SelectPushed", throwIfNotFound: true);
        m_InventoryControls_Up = m_InventoryControls.FindAction("Up", throwIfNotFound: true);
        m_InventoryControls_Down = m_InventoryControls.FindAction("Down", throwIfNotFound: true);
        m_InventoryControls_Left = m_InventoryControls.FindAction("Left", throwIfNotFound: true);
        m_InventoryControls_Right = m_InventoryControls.FindAction("Right", throwIfNotFound: true);
        // TimeControls
        m_TimeControls = asset.FindActionMap("TimeControls", throwIfNotFound: true);
        m_TimeControls_TimeSlow = m_TimeControls.FindAction("TimeSlow", throwIfNotFound: true);
        m_TimeControls_TimeFastForward = m_TimeControls.FindAction("TimeFastForward", throwIfNotFound: true);
        m_TimeControls_TimeStop = m_TimeControls.FindAction("TimeStop", throwIfNotFound: true);
        m_TimeControls_TimeJumpForward = m_TimeControls.FindAction("TimeJumpForward", throwIfNotFound: true);
        // MenuControls
        m_MenuControls = asset.FindActionMap("MenuControls", throwIfNotFound: true);
        m_MenuControls_CancelBack = m_MenuControls.FindAction("Cancel/Back", throwIfNotFound: true);
        // CameraDebugAngles
        m_CameraDebugAngles = asset.FindActionMap("CameraDebugAngles", throwIfNotFound: true);
        m_CameraDebugAngles_CycleAngles = m_CameraDebugAngles.FindAction("CycleAngles", throwIfNotFound: true);
        m_CameraDebugAngles_ChangeAngle = m_CameraDebugAngles.FindAction("ChangeAngle", throwIfNotFound: true);
        m_CameraDebugAngles_ChangeAngleLeft = m_CameraDebugAngles.FindAction("ChangeAngleLeft", throwIfNotFound: true);
        m_CameraDebugAngles_ChangeAngleRight = m_CameraDebugAngles.FindAction("ChangeAngleRight", throwIfNotFound: true);
        // MainMenu
        m_MainMenu = asset.FindActionMap("MainMenu", throwIfNotFound: true);
        m_MainMenu_Up = m_MainMenu.FindAction("Up", throwIfNotFound: true);
        m_MainMenu_Down = m_MainMenu.FindAction("Down", throwIfNotFound: true);
        m_MainMenu_Accept = m_MainMenu.FindAction("Accept", throwIfNotFound: true);
        m_MainMenu_Cancel = m_MainMenu.FindAction("Cancel", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Move;
    private readonly InputAction m_PlayerControls_Jump;
    private readonly InputAction m_PlayerControls_ActivateHook;
    private readonly InputAction m_PlayerControls_ElecArm;
    private readonly InputAction m_PlayerControls_ActivateMagArm;
    private readonly InputAction m_PlayerControls_Interact;
    public struct PlayerControlsActions
    {
        private @PlayerInputAction m_Wrapper;
        public PlayerControlsActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerControls_Move;
        public InputAction @Jump => m_Wrapper.m_PlayerControls_Jump;
        public InputAction @ActivateHook => m_Wrapper.m_PlayerControls_ActivateHook;
        public InputAction @ElecArm => m_Wrapper.m_PlayerControls_ElecArm;
        public InputAction @ActivateMagArm => m_Wrapper.m_PlayerControls_ActivateMagArm;
        public InputAction @Interact => m_Wrapper.m_PlayerControls_Interact;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnJump;
                @ActivateHook.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnActivateHook;
                @ActivateHook.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnActivateHook;
                @ActivateHook.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnActivateHook;
                @ElecArm.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnElecArm;
                @ElecArm.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnElecArm;
                @ElecArm.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnElecArm;
                @ActivateMagArm.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnActivateMagArm;
                @ActivateMagArm.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnActivateMagArm;
                @ActivateMagArm.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnActivateMagArm;
                @Interact.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @ActivateHook.started += instance.OnActivateHook;
                @ActivateHook.performed += instance.OnActivateHook;
                @ActivateHook.canceled += instance.OnActivateHook;
                @ElecArm.started += instance.OnElecArm;
                @ElecArm.performed += instance.OnElecArm;
                @ElecArm.canceled += instance.OnElecArm;
                @ActivateMagArm.started += instance.OnActivateMagArm;
                @ActivateMagArm.performed += instance.OnActivateMagArm;
                @ActivateMagArm.canceled += instance.OnActivateMagArm;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);

    // InventoryControls
    private readonly InputActionMap m_InventoryControls;
    private IInventoryControlsActions m_InventoryControlsActionsCallbackInterface;
    private readonly InputAction m_InventoryControls_LeftBumper;
    private readonly InputAction m_InventoryControls_RightBumper;
    private readonly InputAction m_InventoryControls_LeftSelectCog;
    private readonly InputAction m_InventoryControls_RightSelectCog;
    private readonly InputAction m_InventoryControls_EastButton;
    private readonly InputAction m_InventoryControls_SouthButton;
    private readonly InputAction m_InventoryControls_SelectPushed;
    private readonly InputAction m_InventoryControls_Up;
    private readonly InputAction m_InventoryControls_Down;
    private readonly InputAction m_InventoryControls_Left;
    private readonly InputAction m_InventoryControls_Right;
    public struct InventoryControlsActions
    {
        private @PlayerInputAction m_Wrapper;
        public InventoryControlsActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftBumper => m_Wrapper.m_InventoryControls_LeftBumper;
        public InputAction @RightBumper => m_Wrapper.m_InventoryControls_RightBumper;
        public InputAction @LeftSelectCog => m_Wrapper.m_InventoryControls_LeftSelectCog;
        public InputAction @RightSelectCog => m_Wrapper.m_InventoryControls_RightSelectCog;
        public InputAction @EastButton => m_Wrapper.m_InventoryControls_EastButton;
        public InputAction @SouthButton => m_Wrapper.m_InventoryControls_SouthButton;
        public InputAction @SelectPushed => m_Wrapper.m_InventoryControls_SelectPushed;
        public InputAction @Up => m_Wrapper.m_InventoryControls_Up;
        public InputAction @Down => m_Wrapper.m_InventoryControls_Down;
        public InputAction @Left => m_Wrapper.m_InventoryControls_Left;
        public InputAction @Right => m_Wrapper.m_InventoryControls_Right;
        public InputActionMap Get() { return m_Wrapper.m_InventoryControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryControlsActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryControlsActions instance)
        {
            if (m_Wrapper.m_InventoryControlsActionsCallbackInterface != null)
            {
                @LeftBumper.started -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnLeftBumper;
                @LeftBumper.performed -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnLeftBumper;
                @LeftBumper.canceled -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnLeftBumper;
                @RightBumper.started -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnRightBumper;
                @RightBumper.performed -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnRightBumper;
                @RightBumper.canceled -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnRightBumper;
                @LeftSelectCog.started -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnLeftSelectCog;
                @LeftSelectCog.performed -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnLeftSelectCog;
                @LeftSelectCog.canceled -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnLeftSelectCog;
                @RightSelectCog.started -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnRightSelectCog;
                @RightSelectCog.performed -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnRightSelectCog;
                @RightSelectCog.canceled -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnRightSelectCog;
                @EastButton.started -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnEastButton;
                @EastButton.performed -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnEastButton;
                @EastButton.canceled -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnEastButton;
                @SouthButton.started -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnSouthButton;
                @SouthButton.performed -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnSouthButton;
                @SouthButton.canceled -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnSouthButton;
                @SelectPushed.started -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnSelectPushed;
                @SelectPushed.performed -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnSelectPushed;
                @SelectPushed.canceled -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnSelectPushed;
                @Up.started -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_InventoryControlsActionsCallbackInterface.OnRight;
            }
            m_Wrapper.m_InventoryControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftBumper.started += instance.OnLeftBumper;
                @LeftBumper.performed += instance.OnLeftBumper;
                @LeftBumper.canceled += instance.OnLeftBumper;
                @RightBumper.started += instance.OnRightBumper;
                @RightBumper.performed += instance.OnRightBumper;
                @RightBumper.canceled += instance.OnRightBumper;
                @LeftSelectCog.started += instance.OnLeftSelectCog;
                @LeftSelectCog.performed += instance.OnLeftSelectCog;
                @LeftSelectCog.canceled += instance.OnLeftSelectCog;
                @RightSelectCog.started += instance.OnRightSelectCog;
                @RightSelectCog.performed += instance.OnRightSelectCog;
                @RightSelectCog.canceled += instance.OnRightSelectCog;
                @EastButton.started += instance.OnEastButton;
                @EastButton.performed += instance.OnEastButton;
                @EastButton.canceled += instance.OnEastButton;
                @SouthButton.started += instance.OnSouthButton;
                @SouthButton.performed += instance.OnSouthButton;
                @SouthButton.canceled += instance.OnSouthButton;
                @SelectPushed.started += instance.OnSelectPushed;
                @SelectPushed.performed += instance.OnSelectPushed;
                @SelectPushed.canceled += instance.OnSelectPushed;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
            }
        }
    }
    public InventoryControlsActions @InventoryControls => new InventoryControlsActions(this);

    // TimeControls
    private readonly InputActionMap m_TimeControls;
    private ITimeControlsActions m_TimeControlsActionsCallbackInterface;
    private readonly InputAction m_TimeControls_TimeSlow;
    private readonly InputAction m_TimeControls_TimeFastForward;
    private readonly InputAction m_TimeControls_TimeStop;
    private readonly InputAction m_TimeControls_TimeJumpForward;
    public struct TimeControlsActions
    {
        private @PlayerInputAction m_Wrapper;
        public TimeControlsActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @TimeSlow => m_Wrapper.m_TimeControls_TimeSlow;
        public InputAction @TimeFastForward => m_Wrapper.m_TimeControls_TimeFastForward;
        public InputAction @TimeStop => m_Wrapper.m_TimeControls_TimeStop;
        public InputAction @TimeJumpForward => m_Wrapper.m_TimeControls_TimeJumpForward;
        public InputActionMap Get() { return m_Wrapper.m_TimeControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TimeControlsActions set) { return set.Get(); }
        public void SetCallbacks(ITimeControlsActions instance)
        {
            if (m_Wrapper.m_TimeControlsActionsCallbackInterface != null)
            {
                @TimeSlow.started -= m_Wrapper.m_TimeControlsActionsCallbackInterface.OnTimeSlow;
                @TimeSlow.performed -= m_Wrapper.m_TimeControlsActionsCallbackInterface.OnTimeSlow;
                @TimeSlow.canceled -= m_Wrapper.m_TimeControlsActionsCallbackInterface.OnTimeSlow;
                @TimeFastForward.started -= m_Wrapper.m_TimeControlsActionsCallbackInterface.OnTimeFastForward;
                @TimeFastForward.performed -= m_Wrapper.m_TimeControlsActionsCallbackInterface.OnTimeFastForward;
                @TimeFastForward.canceled -= m_Wrapper.m_TimeControlsActionsCallbackInterface.OnTimeFastForward;
                @TimeStop.started -= m_Wrapper.m_TimeControlsActionsCallbackInterface.OnTimeStop;
                @TimeStop.performed -= m_Wrapper.m_TimeControlsActionsCallbackInterface.OnTimeStop;
                @TimeStop.canceled -= m_Wrapper.m_TimeControlsActionsCallbackInterface.OnTimeStop;
                @TimeJumpForward.started -= m_Wrapper.m_TimeControlsActionsCallbackInterface.OnTimeJumpForward;
                @TimeJumpForward.performed -= m_Wrapper.m_TimeControlsActionsCallbackInterface.OnTimeJumpForward;
                @TimeJumpForward.canceled -= m_Wrapper.m_TimeControlsActionsCallbackInterface.OnTimeJumpForward;
            }
            m_Wrapper.m_TimeControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TimeSlow.started += instance.OnTimeSlow;
                @TimeSlow.performed += instance.OnTimeSlow;
                @TimeSlow.canceled += instance.OnTimeSlow;
                @TimeFastForward.started += instance.OnTimeFastForward;
                @TimeFastForward.performed += instance.OnTimeFastForward;
                @TimeFastForward.canceled += instance.OnTimeFastForward;
                @TimeStop.started += instance.OnTimeStop;
                @TimeStop.performed += instance.OnTimeStop;
                @TimeStop.canceled += instance.OnTimeStop;
                @TimeJumpForward.started += instance.OnTimeJumpForward;
                @TimeJumpForward.performed += instance.OnTimeJumpForward;
                @TimeJumpForward.canceled += instance.OnTimeJumpForward;
            }
        }
    }
    public TimeControlsActions @TimeControls => new TimeControlsActions(this);

    // MenuControls
    private readonly InputActionMap m_MenuControls;
    private IMenuControlsActions m_MenuControlsActionsCallbackInterface;
    private readonly InputAction m_MenuControls_CancelBack;
    public struct MenuControlsActions
    {
        private @PlayerInputAction m_Wrapper;
        public MenuControlsActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @CancelBack => m_Wrapper.m_MenuControls_CancelBack;
        public InputActionMap Get() { return m_Wrapper.m_MenuControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuControlsActions set) { return set.Get(); }
        public void SetCallbacks(IMenuControlsActions instance)
        {
            if (m_Wrapper.m_MenuControlsActionsCallbackInterface != null)
            {
                @CancelBack.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnCancelBack;
                @CancelBack.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnCancelBack;
                @CancelBack.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnCancelBack;
            }
            m_Wrapper.m_MenuControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CancelBack.started += instance.OnCancelBack;
                @CancelBack.performed += instance.OnCancelBack;
                @CancelBack.canceled += instance.OnCancelBack;
            }
        }
    }
    public MenuControlsActions @MenuControls => new MenuControlsActions(this);

    // CameraDebugAngles
    private readonly InputActionMap m_CameraDebugAngles;
    private ICameraDebugAnglesActions m_CameraDebugAnglesActionsCallbackInterface;
    private readonly InputAction m_CameraDebugAngles_CycleAngles;
    private readonly InputAction m_CameraDebugAngles_ChangeAngle;
    private readonly InputAction m_CameraDebugAngles_ChangeAngleLeft;
    private readonly InputAction m_CameraDebugAngles_ChangeAngleRight;
    public struct CameraDebugAnglesActions
    {
        private @PlayerInputAction m_Wrapper;
        public CameraDebugAnglesActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @CycleAngles => m_Wrapper.m_CameraDebugAngles_CycleAngles;
        public InputAction @ChangeAngle => m_Wrapper.m_CameraDebugAngles_ChangeAngle;
        public InputAction @ChangeAngleLeft => m_Wrapper.m_CameraDebugAngles_ChangeAngleLeft;
        public InputAction @ChangeAngleRight => m_Wrapper.m_CameraDebugAngles_ChangeAngleRight;
        public InputActionMap Get() { return m_Wrapper.m_CameraDebugAngles; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraDebugAnglesActions set) { return set.Get(); }
        public void SetCallbacks(ICameraDebugAnglesActions instance)
        {
            if (m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface != null)
            {
                @CycleAngles.started -= m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface.OnCycleAngles;
                @CycleAngles.performed -= m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface.OnCycleAngles;
                @CycleAngles.canceled -= m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface.OnCycleAngles;
                @ChangeAngle.started -= m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface.OnChangeAngle;
                @ChangeAngle.performed -= m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface.OnChangeAngle;
                @ChangeAngle.canceled -= m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface.OnChangeAngle;
                @ChangeAngleLeft.started -= m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface.OnChangeAngleLeft;
                @ChangeAngleLeft.performed -= m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface.OnChangeAngleLeft;
                @ChangeAngleLeft.canceled -= m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface.OnChangeAngleLeft;
                @ChangeAngleRight.started -= m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface.OnChangeAngleRight;
                @ChangeAngleRight.performed -= m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface.OnChangeAngleRight;
                @ChangeAngleRight.canceled -= m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface.OnChangeAngleRight;
            }
            m_Wrapper.m_CameraDebugAnglesActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CycleAngles.started += instance.OnCycleAngles;
                @CycleAngles.performed += instance.OnCycleAngles;
                @CycleAngles.canceled += instance.OnCycleAngles;
                @ChangeAngle.started += instance.OnChangeAngle;
                @ChangeAngle.performed += instance.OnChangeAngle;
                @ChangeAngle.canceled += instance.OnChangeAngle;
                @ChangeAngleLeft.started += instance.OnChangeAngleLeft;
                @ChangeAngleLeft.performed += instance.OnChangeAngleLeft;
                @ChangeAngleLeft.canceled += instance.OnChangeAngleLeft;
                @ChangeAngleRight.started += instance.OnChangeAngleRight;
                @ChangeAngleRight.performed += instance.OnChangeAngleRight;
                @ChangeAngleRight.canceled += instance.OnChangeAngleRight;
            }
        }
    }
    public CameraDebugAnglesActions @CameraDebugAngles => new CameraDebugAnglesActions(this);

    // MainMenu
    private readonly InputActionMap m_MainMenu;
    private IMainMenuActions m_MainMenuActionsCallbackInterface;
    private readonly InputAction m_MainMenu_Up;
    private readonly InputAction m_MainMenu_Down;
    private readonly InputAction m_MainMenu_Accept;
    private readonly InputAction m_MainMenu_Cancel;
    public struct MainMenuActions
    {
        private @PlayerInputAction m_Wrapper;
        public MainMenuActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_MainMenu_Up;
        public InputAction @Down => m_Wrapper.m_MainMenu_Down;
        public InputAction @Accept => m_Wrapper.m_MainMenu_Accept;
        public InputAction @Cancel => m_Wrapper.m_MainMenu_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_MainMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMenuActions set) { return set.Get(); }
        public void SetCallbacks(IMainMenuActions instance)
        {
            if (m_Wrapper.m_MainMenuActionsCallbackInterface != null)
            {
                @Up.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnDown;
                @Accept.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnAccept;
                @Accept.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnAccept;
                @Accept.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnAccept;
                @Cancel.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_MainMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Accept.started += instance.OnAccept;
                @Accept.performed += instance.OnAccept;
                @Accept.canceled += instance.OnAccept;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public MainMenuActions @MainMenu => new MainMenuActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnActivateHook(InputAction.CallbackContext context);
        void OnElecArm(InputAction.CallbackContext context);
        void OnActivateMagArm(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IInventoryControlsActions
    {
        void OnLeftBumper(InputAction.CallbackContext context);
        void OnRightBumper(InputAction.CallbackContext context);
        void OnLeftSelectCog(InputAction.CallbackContext context);
        void OnRightSelectCog(InputAction.CallbackContext context);
        void OnEastButton(InputAction.CallbackContext context);
        void OnSouthButton(InputAction.CallbackContext context);
        void OnSelectPushed(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
    }
    public interface ITimeControlsActions
    {
        void OnTimeSlow(InputAction.CallbackContext context);
        void OnTimeFastForward(InputAction.CallbackContext context);
        void OnTimeStop(InputAction.CallbackContext context);
        void OnTimeJumpForward(InputAction.CallbackContext context);
    }
    public interface IMenuControlsActions
    {
        void OnCancelBack(InputAction.CallbackContext context);
    }
    public interface ICameraDebugAnglesActions
    {
        void OnCycleAngles(InputAction.CallbackContext context);
        void OnChangeAngle(InputAction.CallbackContext context);
        void OnChangeAngleLeft(InputAction.CallbackContext context);
        void OnChangeAngleRight(InputAction.CallbackContext context);
    }
    public interface IMainMenuActions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnAccept(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
}
