
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem.Interactions;

public class BuildingCreator : Singleton <BuildingCreator> 
{
    [SerializeField] Tilemap previewMap, userBuildingGrid;
    PlayerInputs playerInput;


    TileBase tileBase;
    BuildingObjectBase selectedObj;

    Camera _camera;

    Vector2 mousePos;
    Vector3Int currentGridPosition;
    Vector3Int lastGridPosition;

    bool LholdActive,RholdActive;
    Vector3Int holdStartPosition;
    

    BoundsInt bounds;

    protected override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInputs();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        playerInput.Enable();
        
        playerInput.Gameplay.MousePosition.performed += OnMouseMove;

        playerInput.Gameplay.MouseLeftClick.performed += OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.started += OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.canceled += OnLeftClick; 

        playerInput.Gameplay.MouseRightClick.performed += OnRightClick;
        playerInput.Gameplay.MouseRightClick.started += OnRightClick;
        playerInput.Gameplay.MouseRightClick.canceled += OnRightClick;
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Gameplay.MousePosition.performed -= OnMouseMove;

        playerInput.Gameplay.MouseLeftClick.performed -= OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.started -= OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.canceled -= OnLeftClick;

        playerInput.Gameplay.MouseRightClick.performed -= OnRightClick;
        playerInput.Gameplay.MouseRightClick.started -= OnRightClick;
        playerInput.Gameplay.MouseRightClick.canceled -= OnRightClick;
    }

    private BuildingObjectBase SelectedObj
    {
        set
        {
            selectedObj = value;

            tileBase = selectedObj !=null? selectedObj.TileBase : null;

            UpdatePreview();
        }
    }

    private void Update()
    {   
            Vector3 pos = _camera.ScreenToWorldPoint(mousePos);
            Vector3Int gridPos = previewMap.WorldToCell(pos);

            if (gridPos != currentGridPosition)
            {
                lastGridPosition = currentGridPosition;
                currentGridPosition = gridPos;

                UpdatePreview();

                if (LholdActive)
                {
                    HandleDrawing();
                }
                else if (RholdActive)
                {
                    EraseItem();
                }
            
        }
    }

    private void OnMouseMove(InputAction.CallbackContext ctx)
    {
        mousePos = ctx.ReadValue<Vector2>();
    }

    private void OnLeftClick(InputAction.CallbackContext ctx)
    {
        //Debug.Log(ctx.interaction + "/" + ctx.phase);
        if(selectedObj != null && !EventSystem.current.IsPointerOverGameObject())
        {
            if (ctx.phase == InputActionPhase.Started)
            {
                if (ctx.interaction is SlowTapInteraction)
                {
                    //Hold event started
                    LholdActive = true;
                }

                holdStartPosition = currentGridPosition;
                HandleDrawing();
            }
            else
            {
                if (LholdActive)
                {
                    LholdActive = false;
                    //Draw on Release
                    HandleDrawRelease();
                }
            }
        }
    }

    private void OnRightClick(InputAction.CallbackContext ctx)
    {
        
        SelectedObj = null;
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (ctx.phase == InputActionPhase.Started)
            {
                if (ctx.interaction is SlowTapInteraction)
                {
                    //Hold event started
                    RholdActive = true;
                }

                holdStartPosition = currentGridPosition;
                EraseItem();
            }
            else
            {
                if (RholdActive)
                {
                    RholdActive = false;
                    //Erase on Release
                    EraseItem();
                }
            }
        }

    }

    public void ObjectSelected(BuildingObjectBase obj)
    {
        SelectedObj = obj;
        // Set preview where mouse is
        // On click draw
        //On right click cancel drawing
    }

    private void UpdatePreview()
    {
        //Remove old tile if existing
        previewMap.SetTile(lastGridPosition, null);
        //Set current tile to current mouse position tile
        previewMap.SetTile(currentGridPosition, tileBase);
    }

    private void HandleDrawing()
    {
        if(selectedObj != null)
        {
            switch(selectedObj.PlaceType)
            {
                case PlaceType.Single:
                default:
                    DrawItem();
                    break;
                case PlaceType.Line:
                    break;
                case PlaceType.Rectangle:
                    RectangleRenderer();
                    break;
            }
        }
        
    }

    private void HandleDrawRelease()
    {
        if (selectedObj != null)
        {
            switch (selectedObj.PlaceType)
            {
                case PlaceType.Line:
                    break;
                case PlaceType.Rectangle:
                    DrawBounds(userBuildingGrid);
                    previewMap.ClearAllTiles();
                    break;
            }
        }
    }

    private void RectangleRenderer()
    {
        // Render Preview on UI Map, draw real one on Release

        previewMap.ClearAllTiles();

        bounds.xMin = currentGridPosition.x < holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
        bounds.xMax = currentGridPosition.x > holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
        bounds.yMin = currentGridPosition.y < holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;
        bounds.yMax = currentGridPosition.y > holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;

        DrawBounds(previewMap);
    }

    private void DrawBounds(Tilemap map)
    {
        for(int x= bounds.xMin; x<=bounds.xMax; x++)
        {
            for(int y = bounds.yMin; y <= bounds.yMax; y++)
            {
                map.SetTile(new Vector3Int(x, y, 0), tileBase);
            }
        }
    }
    private void DrawItem()
    {
        userBuildingGrid.SetTile(currentGridPosition, tileBase);
    }

    private void EraseItem()
    {
        userBuildingGrid.SetTile(currentGridPosition, null);
    }
}

