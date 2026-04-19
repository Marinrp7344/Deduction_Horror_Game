using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Image_Zooming : MonoBehaviour
{
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float dragSpeed;
    [SerializeField] private float maxZoomBounds;
    [SerializeField] private float minZoomBounds;
    [SerializeField] private float yRectMax;
    [SerializeField] private float xRectMax;
    [SerializeField] private float currentZoomBounds;
    [SerializeField] private float currentYMax;
    [SerializeField] private float currentXMax;
    [SerializeField] private float currentY;
    [SerializeField] private float currentX;

    [SerializeField] private List<RawImage> textures;
    private void CalculateBounds()
    {
        float boundsRatio = maxZoomBounds - minZoomBounds;
        float adjustedZoomBounds = currentZoomBounds - minZoomBounds;
        float currentPercentageZoomed = 1 - (adjustedZoomBounds / boundsRatio);
        currentYMax = xRectMax * currentPercentageZoomed;
        currentXMax = yRectMax * currentPercentageZoomed;
        currentX = currentXMax - (currentX * currentPercentageZoomed);
        currentY = currentYMax - (currentY * currentPercentageZoomed);
        ApplyZoom();
    }

    private void ApplyZoom()
    {
        float x = ApplyRectBounds(currentX, currentXMax);
        float y = ApplyRectBounds(currentY, currentYMax);

        foreach(RawImage texture in textures)
        {
            Rect newUV = texture.uvRect;
            newUV.x = x;
            newUV.y = y;
            newUV.width = currentZoomBounds;
            newUV.height = currentZoomBounds;
            texture.uvRect = newUV;
        }
    }

    private float ApplyRectBounds(float currentValue, float max)
    {
        if(currentValue > max)
        {
            return max;
        }
        else if(currentValue < 0)
        {
            return 0;
        }
        else
        {
            return currentValue;
        }
    }




    public void OnZoom(BaseEventData eventData)
    {
        Debug.Log("Scrolling");
        PointerEventData pointerData = (PointerEventData)eventData;

        float scroll = pointerData.scrollDelta.y;

        if(scroll > 0)
        {
            currentZoomBounds -= zoomSpeed * Time.deltaTime;
        }
        else if( scroll < 0)
        {
            currentZoomBounds += zoomSpeed * Time.deltaTime;
        }

        if(currentZoomBounds < minZoomBounds)
        {
            currentZoomBounds = minZoomBounds;
        }
        else if(currentZoomBounds > maxZoomBounds)
        {
            currentZoomBounds = maxZoomBounds;
        }

        CalculateBounds();
    }

    public void DragImage(BaseEventData eventData)
    {
        PointerEventData pointerData = (PointerEventData)eventData;

        Vector2 delta = pointerData.delta;

        float moveX = delta.x * currentZoomBounds * dragSpeed;
        float moveY = delta.y * currentZoomBounds * dragSpeed;

        currentX -= moveX;
        currentY -= moveY;

        ApplyZoom();
    }



}
