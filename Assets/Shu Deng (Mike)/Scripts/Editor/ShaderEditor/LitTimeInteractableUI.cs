﻿using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEditor;
using UnityEditor.Experimental.Rendering.HDPipeline;

namespace com.savethedate.editor.rendering
{
    class LitTimeInteractableGUI : BaseLitGUI
    {
        protected override uint defaultExpandedState { get { return (uint)(Expandable.Base | Expandable.Input | Expandable.VertexAnimation | Expandable.Detail | Expandable.Emissive | Expandable.Transparency | Expandable.Tesselation); } }

        protected static class Styles
        {
            public static string InputsText = "Surface Inputs";

            public static GUIContent baseColorText = new GUIContent("Base Map", "Specifies the base color (RGB) and opacity (A) of the Material.");

            public static GUIContent metallicText = new GUIContent("Metallic", "Controls the scale factor for the Material's metallic effect.");
            public static GUIContent smoothnessText = new GUIContent("Smoothness", "Controls the scale factor for the Material's smoothness.");
            public static GUIContent smoothnessRemappingText = new GUIContent("Smoothness Remapping", "Controls a remap for the smoothness channel in the Mask Map.");
            public static GUIContent aoRemappingText = new GUIContent("Ambient Occlusion Remapping", "Controls a remap for the ambient occlusion channel in the Mask Map.");
            public static GUIContent maskMapSText = new GUIContent("Mask Map", "Specifies the Mask Map for this Material - Metallic (R), Ambient occlusion (G), Detail mask (B), Smoothness (A).");
            public static GUIContent maskMapSpecularText = new GUIContent("Mask Map", "Specifies the Mask Map for this Material - Ambient occlusion (G), Detail mask (B), Smoothness (A).");

            public static GUIContent normalMapSpaceText = new GUIContent("Normal Map Space", "");
            public static GUIContent normalMapText = new GUIContent("Normal Map", "Specifies the Normal Map for this Material (BC7/BC5/DXT5(nm)) and controls its strength.");
            public static GUIContent normalMapOSText = new GUIContent("Normal Map OS", "Specifies the object space Normal Map (BC7/DXT1/RGB).");
            public static GUIContent bentNormalMapText = new GUIContent("Bent normal map", "Specifies the cosine weighted Bent Normal Map (BC7/BC5/DXT5(nm)) for this Material. Use only with indirect diffuse lighting (Lightmaps and Light Probes).");
            public static GUIContent bentNormalMapOSText = new GUIContent("Bent normal map OS", "Specifies the object space Bent Normal Map (BC7/DXT1/RGB) for this Material. Use only with indirect diffuse lighting (Lightmaps and Light Probes).");

            // Height
            public static GUIContent heightMapText = new GUIContent("Height Map", "Specifies the Height Map (R) for this Material.\nFor floating point textures, set the Min, Max, and base values to 0, 1, and 0 respectively.");
            public static GUIContent heightMapCenterText = new GUIContent("Base", "Controls the base of the Height Map (between 0 and 1).");
            public static GUIContent heightMapMinText = new GUIContent("Min", "Sets the minimum value in the Height Map (in centimeters).");
            public static GUIContent heightMapMaxText = new GUIContent("Max", "Sets the maximum value in the Height Map (in centimeters).");
            public static GUIContent heightMapAmplitudeText = new GUIContent("Amplitude", "Sets the amplitude of the Height Map (in centimeters).");
            public static GUIContent heightMapOffsetText = new GUIContent("Offset", "Sets the offset HDRP applies to the Height Map (in centimeters).");
            public static GUIContent heightMapParametrization = new GUIContent("Parametrization", "Specifies the parametrization method for the Height Map.");

            public static GUIContent tangentMapText = new GUIContent("Tangent Map", "Specifies the Tangent Map (BC7/BC5/DXT5(nm)) for this Material.");
            public static GUIContent tangentMapOSText = new GUIContent("Tangent Map OS", "Specifies the object space Tangent Map (BC7/DXT1/RGB) for this Material.");
            public static GUIContent anisotropyText = new GUIContent("Anisotropy", "Controls the scale factor for anisotropy.");
            public static GUIContent anisotropyMapText = new GUIContent("Anisotropy Map", "Specifies the Anisotropy Map(R) for this Material.");

            public static GUIContent UVBaseMappingText = new GUIContent("Base UV Mapping", "");
            public static GUIContent texWorldScaleText = new GUIContent("World Scale", "Sets the tiling factor HDRP applies to Planar/Trilinear mapping.");

            // Details
            public static string detailText = "Detail Inputs";
            public static GUIContent UVDetailMappingText = new GUIContent("Detail UV Mapping", "");
            public static GUIContent detailMapNormalText = new GUIContent("Detail Map", "Specifies the Detail Map albedo (R) Normal map y-axis (G) Smoothness (B) Normal map x-axis (A) - Neutral value is (0.5, 0.5, 0.5, 0.5)");
            public static GUIContent detailAlbedoScaleText = new GUIContent("Detail Albedo Scale", "Controls the scale factor for the Detail Map's Albedo.");
            public static GUIContent detailNormalScaleText = new GUIContent("Detail Normal Scale", "Controls the scale factor for the Detail Map's Normal map.");
            public static GUIContent detailSmoothnessScaleText = new GUIContent("Detail Smoothness Scale", "Controls the scale factor for the Detail Map's Smoothness.");
            public static GUIContent linkDetailsWithBaseText = new GUIContent("Lock to Base Tiling/Offset", "When enabled, HDRP locks the Detail's Tiling/Offset values to the Base Tiling/Offset.");

            // Subsurface
            public static GUIContent diffusionProfileText = new GUIContent("Diffusion Profile", "Specifies the Diffusion Profie HDRP uses to determine the behavior of the subsurface scattering/transmission effect.");
            public static GUIContent subsurfaceMaskText = new GUIContent("Subsurface Mask", "Controls the overall strength of the subsurface scattering effect.");
            public static GUIContent subsurfaceMaskMapText = new GUIContent("Subsurface Mask Map", "Specifies the Subsurface mask map (R) for this Material - This map controls the strength of the subsurface scattering effect.");
            public static GUIContent thicknessText = new GUIContent("Thickness", "Controls the strength of the Thickness Map, low values allow some light to transmit through the object.");
            public static GUIContent thicknessMapText = new GUIContent("Thickness Map", "Specifies the Thickness Map (R) for this Material - This map describes the thickness of the object. When subsurface scattering is enabled, low values allow some light to transmit through the object.");
            public static GUIContent thicknessRemapText = new GUIContent("Thickness Remap", "Controls a remap for the Thickness Map from [0, 1] to the specified range.");

            // Iridescence
            public static GUIContent iridescenceMaskText = new GUIContent("Iridescence Mask", "Specifies the Iridescence Mask (R) for this Material - This map controls the intensity of the iridescence.");
            public static GUIContent iridescenceThicknessText = new GUIContent("Iridescence Layer Thickness");
            public static GUIContent iridescenceThicknessMapText = new GUIContent("Iridescence Layer Thickness map", "Specifies the Iridescence Layer Thickness map (R) for this Material.");
            public static GUIContent iridescenceThicknessRemapText = new GUIContent("Iridescence Layer Thickness remap");

            // Clear Coat
            public static GUIContent coatMaskText = new GUIContent("Coat Mask", "Attenuate the coating effect.");

            // Specular color
            public static GUIContent energyConservingSpecularColorText = new GUIContent("Energy Conserving Specular Color", "When enabled, HDRP simulates energy conservation when using Specular Color mode. This results in high Specular Color values producing lower Diffuse Color values.");
            public static GUIContent specularColorText = new GUIContent("Specular Color", "Specifies the Specular color (RGB) of this Material.");

            // Specular occlusion
            public static GUIContent enableSpecularOcclusionText = new GUIContent("Specular Occlusion From Bent Normal", "Requires cosine weighted bent normal and cosine weighted ambient occlusion. Specular occlusion for Reflection Probe");
            public static GUIContent specularOcclusionWarning = new GUIContent("Require a cosine weighted bent normal and ambient occlusion maps");

            // Emissive
            public static string emissiveLabelText = "Emission Inputs";

            public static GUIContent emissiveText = new GUIContent("Emission Map", "Specifies the Emission Map (RGB) for this Material. Uses Candelas per square meter for units.");
            public static GUIContent albedoAffectEmissiveText = new GUIContent("Emission Multiply With Base", "When enabled, HDRP multiplies the emission color by the albedo.");
            public static GUIContent useEmissiveIntensityText = new GUIContent("Use Emission Intensity", "Specifies whether to use to a HDR color or a LDR color with a separate multiplier.");
            public static GUIContent emissiveIntensityText = new GUIContent("Emission Intensity", "");
            public static GUIContent emissiveIntensityFromHDRColorText = new GUIContent("The emission intensity is from the HDR color picker in luminance", "");
            public static GUIContent emissiveExposureWeightText = new GUIContent("Exposure Weight", "Control the percentage of emission to expose.");

            public static GUIContent normalMapSpaceWarning = new GUIContent("HDRP does not support object space normals with triplanar mapping.");

            // Transparency
            public static string refractionModelText = "Refraction Model";
            public static GUIContent refractionIorText = new GUIContent("Index Of Refraction", "Controls the index of refraction for this Material.");
            public static GUIContent refractionThicknessText = new GUIContent("Refraction Thickness", "Controls the thickness for rough refraction.");
            public static GUIContent refractionThicknessMultiplierText = new GUIContent("Refraction Thickness Multiplier", "Sets an overall thickness multiplier in meters.");
            public static GUIContent refractionThicknessMapText = new GUIContent("Refraction Thickness Map", "Specifies the Refraction Thickness Map (R) for this Material - This acts as a thickness multiplier map.");
            // Transparency absorption
            public static GUIContent transmittanceColorText = new GUIContent("Transmittance Color", "Specifies the Transmittance Color (RGB) for this Material.");
            public static GUIContent atDistanceText = new GUIContent("Transmittance Absorption Distance", "Sets the absorption distance reference in meters.");

            public static GUIContent perPixelDisplacementDetailsWarning = new GUIContent("For pixel displacement to work correctly, details and base map must use the same UV mapping.");
        }

        // Lit shader is not layered but some layered materials inherit from it. In order to share code we need LitUI to account for this.
        protected const int kMaxLayerCount = 4;

        protected int m_LayerCount = 1;
        protected string[] m_PropertySuffixes = { "", "", "", "" };

        public enum UVBaseMapping
        {
            UV0,
            UV1,
            UV2,
            UV3,
            Planar,
            Triplanar
        }

        public enum NormalMapSpace
        {
            TangentSpace,
            ObjectSpace,
        }

        public enum HeightmapMode
        {
            Parallax,
            Displacement,
        }

        public enum UVDetailMapping
        {
            UV0,
            UV1,
            UV2,
            UV3
        }

        protected MaterialProperty[] UVBase = new MaterialProperty[kMaxLayerCount];
        protected const string kUVBase = "_UVBase";
        protected MaterialProperty[] TexWorldScale = new MaterialProperty[kMaxLayerCount];
        protected const string kTexWorldScale = "_TexWorldScale";
        protected MaterialProperty[] InvTilingScale = new MaterialProperty[kMaxLayerCount];
        protected const string kInvTilingScale = "_InvTilingScale";
        protected MaterialProperty[] UVMappingMask = new MaterialProperty[kMaxLayerCount];
        protected const string kUVMappingMask = "_UVMappingMask";

        protected MaterialProperty[] baseColor = new MaterialProperty[kMaxLayerCount];
        protected const string kBaseColor = "_BaseColor";
        protected MaterialProperty[] baseColorMap = new MaterialProperty[kMaxLayerCount];
        protected const string kBaseColorMap = "_BaseColorMap";
        protected MaterialProperty[] metallic = new MaterialProperty[kMaxLayerCount];
        protected const string kMetallic = "_Metallic";
        protected MaterialProperty[] smoothness = new MaterialProperty[kMaxLayerCount];
        protected const string kSmoothness = "_Smoothness";
        protected MaterialProperty[] smoothnessRemapMin = new MaterialProperty[kMaxLayerCount];
        protected const string kSmoothnessRemapMin = "_SmoothnessRemapMin";
        protected MaterialProperty[] smoothnessRemapMax = new MaterialProperty[kMaxLayerCount];
        protected const string kSmoothnessRemapMax = "_SmoothnessRemapMax";
        protected MaterialProperty[] aoRemapMin = new MaterialProperty[kMaxLayerCount];
        protected const string kAORemapMin = "_AORemapMin";
        protected MaterialProperty[] aoRemapMax = new MaterialProperty[kMaxLayerCount];
        protected const string kAORemapMax = "_AORemapMax";
        protected MaterialProperty[] maskMap = new MaterialProperty[kMaxLayerCount];
        protected const string kMaskMap = "_MaskMap";
        protected MaterialProperty[] normalScale = new MaterialProperty[kMaxLayerCount];
        protected const string kNormalScale = "_NormalScale";
        protected MaterialProperty[] normalMap = new MaterialProperty[kMaxLayerCount];
        protected const string kNormalMap = "_NormalMap";
        protected MaterialProperty[] normalMapOS = new MaterialProperty[kMaxLayerCount];
        protected const string kNormalMapOS = "_NormalMapOS";
        protected MaterialProperty[] bentNormalMap = new MaterialProperty[kMaxLayerCount];
        protected const string kBentNormalMap = "_BentNormalMap";
        protected MaterialProperty[] bentNormalMapOS = new MaterialProperty[kMaxLayerCount];
        protected const string kBentNormalMapOS = "_BentNormalMapOS";
        protected MaterialProperty[] normalMapSpace = new MaterialProperty[kMaxLayerCount];
        protected const string kNormalMapSpace = "_NormalMapSpace";

        protected MaterialProperty[] heightMap = new MaterialProperty[kMaxLayerCount];
        protected const string kHeightMap = "_HeightMap";
        protected MaterialProperty[] heightAmplitude = new MaterialProperty[kMaxLayerCount];
        protected const string kHeightAmplitude = "_HeightAmplitude";
        protected MaterialProperty[] heightCenter = new MaterialProperty[kMaxLayerCount];
        protected const string kHeightCenter = "_HeightCenter";
        protected MaterialProperty[] heightPoMAmplitude = new MaterialProperty[kMaxLayerCount];
        protected const string kHeightPoMAmplitude = "_HeightPoMAmplitude";
        protected MaterialProperty[] heightTessCenter = new MaterialProperty[kMaxLayerCount];
        protected const string kHeightTessCenter = "_HeightTessCenter";
        protected MaterialProperty[] heightTessAmplitude = new MaterialProperty[kMaxLayerCount];
        protected const string kHeightTessAmplitude = "_HeightTessAmplitude";
        protected MaterialProperty[] heightMin = new MaterialProperty[kMaxLayerCount];
        protected const string kHeightMin = "_HeightMin";
        protected MaterialProperty[] heightMax = new MaterialProperty[kMaxLayerCount];
        protected const string kHeightMax = "_HeightMax";
        protected MaterialProperty[] heightOffset = new MaterialProperty[kMaxLayerCount];
        protected const string kHeightOffset = "_HeightOffset";
        protected MaterialProperty[] heightParametrization = new MaterialProperty[kMaxLayerCount];
        protected const string kHeightParametrization = "_HeightMapParametrization";

        protected MaterialProperty[] diffusionProfileHash = new MaterialProperty[kMaxLayerCount];
        protected const string kDiffusionProfileHash = "_DiffusionProfileHash";
        protected MaterialProperty[] diffusionProfileAsset = new MaterialProperty[kMaxLayerCount];
        protected const string kDiffusionProfileAsset = "_DiffusionProfileAsset";
        protected MaterialProperty[] subsurfaceMask = new MaterialProperty[kMaxLayerCount];
        protected const string kSubsurfaceMask = "_SubsurfaceMask";
        protected MaterialProperty[] subsurfaceMaskMap = new MaterialProperty[kMaxLayerCount];
        protected const string kSubsurfaceMaskMap = "_SubsurfaceMaskMap";
        protected MaterialProperty[] thickness = new MaterialProperty[kMaxLayerCount];
        protected const string kThickness = "_Thickness";
        protected MaterialProperty[] thicknessMap = new MaterialProperty[kMaxLayerCount];
        protected const string kThicknessMap = "_ThicknessMap";
        protected MaterialProperty[] thicknessRemap = new MaterialProperty[kMaxLayerCount];
        protected const string kThicknessRemap = "_ThicknessRemap";

        protected MaterialProperty[] UVDetail = new MaterialProperty[kMaxLayerCount];
        protected const string kUVDetail = "_UVDetail";
        protected MaterialProperty[] UVDetailsMappingMask = new MaterialProperty[kMaxLayerCount];
        protected const string kUVDetailsMappingMask = "_UVDetailsMappingMask";
        protected MaterialProperty[] detailMap = new MaterialProperty[kMaxLayerCount];
        protected const string kDetailMap = "_DetailMap";
        protected MaterialProperty[] linkDetailsWithBase = new MaterialProperty[kMaxLayerCount];
        protected const string kLinkDetailsWithBase = "_LinkDetailsWithBase";
        protected MaterialProperty[] detailAlbedoScale = new MaterialProperty[kMaxLayerCount];
        protected const string kDetailAlbedoScale = "_DetailAlbedoScale";
        protected MaterialProperty[] detailNormalScale = new MaterialProperty[kMaxLayerCount];
        protected const string kDetailNormalScale = "_DetailNormalScale";
        protected MaterialProperty[] detailSmoothnessScale = new MaterialProperty[kMaxLayerCount];
        protected const string kDetailSmoothnessScale = "_DetailSmoothnessScale";

        protected MaterialProperty energyConservingSpecularColor = null;
        protected const string kEnergyConservingSpecularColor = "_EnergyConservingSpecularColor";
        protected MaterialProperty specularColor = null;
        protected const string kSpecularColor = "_SpecularColor";
        protected MaterialProperty specularColorMap = null;
        protected const string kSpecularColorMap = "_SpecularColorMap";

        protected MaterialProperty tangentMap = null;
        protected const string kTangentMap = "_TangentMap";
        protected MaterialProperty tangentMapOS = null;
        protected const string kTangentMapOS = "_TangentMapOS";
        protected MaterialProperty anisotropy = null;
        protected const string kAnisotropy = "_Anisotropy";
        protected MaterialProperty anisotropyMap = null;
        protected const string kAnisotropyMap = "_AnisotropyMap";

        protected MaterialProperty iridescenceMask = null;
        protected const string kIridescenceMask = "_IridescenceMask";
        protected MaterialProperty iridescenceMaskMap = null;
        protected const string kIridescenceMaskMap = "_IridescenceMaskMap";
        protected MaterialProperty iridescenceThickness = null;
        protected const string kIridescenceThickness = "_IridescenceThickness";
        protected MaterialProperty iridescenceThicknessMap = null;
        protected const string kIridescenceThicknessMap = "_IridescenceThicknessMap";
        protected MaterialProperty iridescenceThicknessRemap = null;
        protected const string kIridescenceThicknessRemap = "_IridescenceThicknessRemap";

        protected MaterialProperty coatMask = null;
        protected const string kCoatMask = "_CoatMask";
        protected MaterialProperty coatMaskMap = null;
        protected const string kCoatMaskMap = "_CoatMaskMap";

        protected MaterialProperty emissiveColorMode = null;
        protected const string kEmissiveColorMode = "_EmissiveColorMode";
        protected MaterialProperty emissiveColor = null;
        protected const string kEmissiveColor = "_EmissiveColor";
        protected MaterialProperty emissiveColorMap = null;
        protected const string kEmissiveColorMap = "_EmissiveColorMap";
        protected MaterialProperty albedoAffectEmissive = null;
        protected const string kAlbedoAffectEmissive = "_AlbedoAffectEmissive";
        protected MaterialProperty UVEmissive = null;
        protected const string kUVEmissive = "_UVEmissive";
        protected MaterialProperty TexWorldScaleEmissive = null;
        protected const string kTexWorldScaleEmissive = "_TexWorldScaleEmissive";
        protected MaterialProperty UVMappingMaskEmissive = null;
        protected const string kUVMappingMaskEmissive = "_UVMappingMaskEmissive";
        protected MaterialProperty emissiveIntensity = null;
        protected const string kEmissiveIntensity = "_EmissiveIntensity";
        protected MaterialProperty emissiveColorLDR = null;
        protected const string kEmissiveColorLDR = "_EmissiveColorLDR";
        protected MaterialProperty emissiveIntensityUnit = null;
        protected const string kEmissiveIntensityUnit = "_EmissiveIntensityUnit";
        protected MaterialProperty emissiveExposureWeight = null;
        protected const string kemissiveExposureWeight = "_EmissiveExposureWeight";
        protected MaterialProperty useEmissiveIntensity = null;
        protected const string kUseEmissiveIntensity = "_UseEmissiveIntensity";

        protected MaterialProperty enableSpecularOcclusion = null;
        protected const string kEnableSpecularOcclusion = "_EnableSpecularOcclusion";

        // START: Added by Shu Deng (Mike)
        // END: Added by Shu Deng (Mike)

        // transparency params
        protected MaterialProperty ior = null;
        protected const string kIor = "_Ior";
        protected MaterialProperty transmittanceColor = null;
        protected const string kTransmittanceColor = "_TransmittanceColor";
        protected MaterialProperty transmittanceColorMap = null;
        protected const string kTransmittanceColorMap = "_TransmittanceColorMap";
        protected MaterialProperty atDistance = null;
        protected const string kATDistance = "_ATDistance";
        protected MaterialProperty thicknessMultiplier = null;
        protected const string kThicknessMultiplier = "_ThicknessMultiplier";
        protected MaterialProperty refractionModel = null;
        protected const string kRefractionModel = "_RefractionModel";
        protected MaterialProperty ssrefractionProjectionModel = null;
        protected const string kSSRefractionProjectionModel = "_SSRefractionProjectionModel";

        protected override bool showBlendModePopup
            => refractionModel == null
            || refractionModel.floatValue == 0f
            || HDRenderQueue.k_RenderQueue_PreRefraction.Contains(renderQueue);

        protected override bool showPreRefractionPass
            => refractionModel == null
            || refractionModel.floatValue == 0f;

        protected override bool showAfterPostProcessPass => false;
        protected override bool showLowResolutionPass => true;

        protected void FindMaterialLayerProperties(MaterialProperty[] props)
        {
            for (int i = 0; i < m_LayerCount; ++i)
            {
                UVBase[i] = FindProperty(string.Format("{0}{1}", kUVBase, m_PropertySuffixes[i]), props);
                TexWorldScale[i] = FindProperty(string.Format("{0}{1}", kTexWorldScale, m_PropertySuffixes[i]), props);
                InvTilingScale[i] = FindProperty(string.Format("{0}{1}", kInvTilingScale, m_PropertySuffixes[i]), props);
                UVMappingMask[i] = FindProperty(string.Format("{0}{1}", kUVMappingMask, m_PropertySuffixes[i]), props);

                baseColor[i] = FindProperty(string.Format("{0}{1}", kBaseColor, m_PropertySuffixes[i]), props);
                baseColorMap[i] = FindProperty(string.Format("{0}{1}", kBaseColorMap, m_PropertySuffixes[i]), props);
                metallic[i] = FindProperty(string.Format("{0}{1}", kMetallic, m_PropertySuffixes[i]), props);
                smoothness[i] = FindProperty(string.Format("{0}{1}", kSmoothness, m_PropertySuffixes[i]), props);
                smoothnessRemapMin[i] = FindProperty(string.Format("{0}{1}", kSmoothnessRemapMin, m_PropertySuffixes[i]), props);
                smoothnessRemapMax[i] = FindProperty(string.Format("{0}{1}", kSmoothnessRemapMax, m_PropertySuffixes[i]), props);
                aoRemapMin[i] = FindProperty(string.Format("{0}{1}", kAORemapMin, m_PropertySuffixes[i]), props);
                aoRemapMax[i] = FindProperty(string.Format("{0}{1}", kAORemapMax, m_PropertySuffixes[i]), props);
                maskMap[i] = FindProperty(string.Format("{0}{1}", kMaskMap, m_PropertySuffixes[i]), props);
                normalMap[i] = FindProperty(string.Format("{0}{1}", kNormalMap, m_PropertySuffixes[i]), props);
                normalMapOS[i] = FindProperty(string.Format("{0}{1}", kNormalMapOS, m_PropertySuffixes[i]), props);
                normalScale[i] = FindProperty(string.Format("{0}{1}", kNormalScale, m_PropertySuffixes[i]), props);
                bentNormalMap[i] = FindProperty(string.Format("{0}{1}", kBentNormalMap, m_PropertySuffixes[i]), props);
                bentNormalMapOS[i] = FindProperty(string.Format("{0}{1}", kBentNormalMapOS, m_PropertySuffixes[i]), props);
                normalMapSpace[i] = FindProperty(string.Format("{0}{1}", kNormalMapSpace, m_PropertySuffixes[i]), props);

                // Height
                heightMap[i] = FindProperty(string.Format("{0}{1}", kHeightMap, m_PropertySuffixes[i]), props);
                heightAmplitude[i] = FindProperty(string.Format("{0}{1}", kHeightAmplitude, m_PropertySuffixes[i]), props);
                heightCenter[i] = FindProperty(string.Format("{0}{1}", kHeightCenter, m_PropertySuffixes[i]), props);
                heightPoMAmplitude[i] = FindProperty(string.Format("{0}{1}", kHeightPoMAmplitude, m_PropertySuffixes[i]), props);
                heightMin[i] = FindProperty(string.Format("{0}{1}", kHeightMin, m_PropertySuffixes[i]), props);
                heightMax[i] = FindProperty(string.Format("{0}{1}", kHeightMax, m_PropertySuffixes[i]), props);
                heightTessCenter[i] = FindProperty(string.Format("{0}{1}", kHeightTessCenter, m_PropertySuffixes[i]), props);
                heightTessAmplitude[i] = FindProperty(string.Format("{0}{1}", kHeightTessAmplitude, m_PropertySuffixes[i]), props);
                heightOffset[i] = FindProperty(string.Format("{0}{1}", kHeightOffset, m_PropertySuffixes[i]), props);
                heightParametrization[i] = FindProperty(string.Format("{0}{1}", kHeightParametrization, m_PropertySuffixes[i]), props);

                // Sub surface
                diffusionProfileHash[i] = FindProperty(string.Format("{0}{1}", kDiffusionProfileHash, m_PropertySuffixes[i]), props);
                diffusionProfileAsset[i] = FindProperty(string.Format("{0}{1}", kDiffusionProfileAsset, m_PropertySuffixes[i]), props);
                subsurfaceMask[i] = FindProperty(string.Format("{0}{1}", kSubsurfaceMask, m_PropertySuffixes[i]), props);
                subsurfaceMaskMap[i] = FindProperty(string.Format("{0}{1}", kSubsurfaceMaskMap, m_PropertySuffixes[i]), props);
                thickness[i] = FindProperty(string.Format("{0}{1}", kThickness, m_PropertySuffixes[i]), props);
                thicknessMap[i] = FindProperty(string.Format("{0}{1}", kThicknessMap, m_PropertySuffixes[i]), props);
                thicknessRemap[i] = FindProperty(string.Format("{0}{1}", kThicknessRemap, m_PropertySuffixes[i]), props);

                // Details
                UVDetail[i] = FindProperty(string.Format("{0}{1}", kUVDetail, m_PropertySuffixes[i]), props);
                UVDetailsMappingMask[i] = FindProperty(string.Format("{0}{1}", kUVDetailsMappingMask, m_PropertySuffixes[i]), props);
                linkDetailsWithBase[i] = FindProperty(string.Format("{0}{1}", kLinkDetailsWithBase, m_PropertySuffixes[i]), props);
                detailMap[i] = FindProperty(string.Format("{0}{1}", kDetailMap, m_PropertySuffixes[i]), props);
                detailAlbedoScale[i] = FindProperty(string.Format("{0}{1}", kDetailAlbedoScale, m_PropertySuffixes[i]), props);
                detailNormalScale[i] = FindProperty(string.Format("{0}{1}", kDetailNormalScale, m_PropertySuffixes[i]), props);
                detailSmoothnessScale[i] = FindProperty(string.Format("{0}{1}", kDetailSmoothnessScale, m_PropertySuffixes[i]), props);
            }
        }

        protected void FindMaterialEmissiveProperties(MaterialProperty[] props)
        {
            emissiveColorMode = FindProperty(kEmissiveColorMode, props);
            emissiveColor = FindProperty(kEmissiveColor, props);
            emissiveColorMap = FindProperty(kEmissiveColorMap, props);
            albedoAffectEmissive = FindProperty(kAlbedoAffectEmissive, props);

            UVEmissive = FindProperty(kUVEmissive, props);
            TexWorldScaleEmissive = FindProperty(kTexWorldScaleEmissive, props);
            UVMappingMaskEmissive = FindProperty(kUVMappingMaskEmissive, props);

            emissiveIntensityUnit = FindProperty(kEmissiveIntensityUnit, props);
            emissiveIntensity = FindProperty(kEmissiveIntensity, props);
            emissiveExposureWeight = FindProperty(kemissiveExposureWeight, props);
            emissiveColorLDR = FindProperty(kEmissiveColorLDR, props);
            useEmissiveIntensity = FindProperty(kUseEmissiveIntensity, props);

            enableSpecularOcclusion = FindProperty(kEnableSpecularOcclusion, props);

            // START: Added by Shu Deng (Mike)
            // END: Added by Shu Deng (Mike)
        }

        protected override void FindMaterialProperties(MaterialProperty[] props)
        {
            FindMaterialLayerProperties(props);
            FindMaterialEmissiveProperties(props);

            // The next properties are only supported for regular Lit shader (not layered ones) because it's complicated to blend those parameters if they are different on a per layer basis.

            // Specular Color
            energyConservingSpecularColor = FindProperty(kEnergyConservingSpecularColor, props);
            specularColor = FindProperty(kSpecularColor, props);
            specularColorMap = FindProperty(kSpecularColorMap, props);

            // Anisotropy
            tangentMap = FindProperty(kTangentMap, props);
            tangentMapOS = FindProperty(kTangentMapOS, props);
            anisotropy = FindProperty(kAnisotropy, props);
            anisotropyMap = FindProperty(kAnisotropyMap, props);

            // Iridescence
            iridescenceMask = FindProperty(kIridescenceMask, props);
            iridescenceMaskMap = FindProperty(kIridescenceMaskMap, props);
            iridescenceThickness = FindProperty(kIridescenceThickness, props);
            iridescenceThicknessMap = FindProperty(kIridescenceThicknessMap, props);
            iridescenceThicknessRemap = FindProperty(kIridescenceThicknessRemap, props);

            // clear coat
            coatMask = FindProperty(kCoatMask, props);
            coatMaskMap = FindProperty(kCoatMaskMap, props);

            // Transparency
            refractionModel = FindProperty(kRefractionModel, props, false);
            ssrefractionProjectionModel = FindProperty(kSSRefractionProjectionModel, props, false);
            transmittanceColor = FindProperty(kTransmittanceColor, props, false);
            transmittanceColorMap = FindProperty(kTransmittanceColorMap, props, false);
            atDistance = FindProperty(kATDistance, props, false);
            thicknessMultiplier = FindProperty(kThicknessMultiplier, props, false);
            ior = FindProperty(kIor, props, false);
            // We reuse thickness from SSS
        }

        protected void ShaderSpecularColorInputGUI(Material material)
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.specularColorText, specularColorMap, specularColor);
            EditorGUI.indentLevel++;
            m_MaterialEditor.ShaderProperty(energyConservingSpecularColor, Styles.energyConservingSpecularColorText);
            EditorGUI.indentLevel--;
        }

        protected void ShaderSSSAndTransmissionInputGUI(Material material, int layerIndex)
        {
            var hdPipeline = RenderPipelineManager.currentPipeline as HDRenderPipeline;

            if (hdPipeline == null)
                return;

            using (var scope = new EditorGUI.ChangeCheckScope())
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    // We can't cache these fields because of several edge cases like undo/redo or pressing escape in the object picker
                    string guid = HDUtils.ConvertVector4ToGUID(diffusionProfileAsset[layerIndex].vectorValue);
                    DiffusionProfileSettings diffusionProfile = AssetDatabase.LoadAssetAtPath<DiffusionProfileSettings>(AssetDatabase.GUIDToAssetPath(guid));

                    // is it okay to do this every frame ?
                    using (var changeScope = new EditorGUI.ChangeCheckScope())
                    {
                        diffusionProfile = (DiffusionProfileSettings)EditorGUILayout.ObjectField(Styles.diffusionProfileText, diffusionProfile, typeof(DiffusionProfileSettings), false);
                        if (changeScope.changed)
                        {
                            Vector4 newGuid = Vector4.zero;
                            float hash = 0;

                            if (diffusionProfile != null)
                            {
                                guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(diffusionProfile));
                                newGuid = HDUtils.ConvertGUIDToVector4(guid);
                                hash = HDShadowUtils.Asfloat(diffusionProfile.profile.hash);
                            }

                            // encode back GUID and it's hash
                            diffusionProfileAsset[layerIndex].vectorValue = newGuid;
                            diffusionProfileHash[layerIndex].floatValue = hash;
                        }
                    }
                }
            }

            if ((int)materialID.floatValue == (int)MaterialId.LitSSS)
            {
                m_MaterialEditor.ShaderProperty(subsurfaceMask[layerIndex], Styles.subsurfaceMaskText);
                m_MaterialEditor.TexturePropertySingleLine(Styles.subsurfaceMaskMapText, subsurfaceMaskMap[layerIndex]);
            }

            if ((int)materialID.floatValue == (int)MaterialId.LitTranslucent ||
                ((int)materialID.floatValue == (int)MaterialId.LitSSS && transmissionEnable.floatValue > 0.0f))
            {
                m_MaterialEditor.TexturePropertySingleLine(Styles.thicknessMapText, thicknessMap[layerIndex]);
                if (thicknessMap[layerIndex].textureValue != null)
                {
                    // Display the remap of texture values.
                    Vector2 remap = thicknessRemap[layerIndex].vectorValue;
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.MinMaxSlider(Styles.thicknessRemapText, ref remap.x, ref remap.y, 0.0f, 1.0f);
                    if (EditorGUI.EndChangeCheck())
                    {
                        thicknessRemap[layerIndex].vectorValue = remap;
                    }
                }
                else
                {
                    // Allow the user to set the constant value of thickness if no thickness map is provided.
                    m_MaterialEditor.ShaderProperty(thickness[layerIndex], Styles.thicknessText);
                }
            }
        }

        protected void ShaderIridescenceInputGUI()
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.iridescenceMaskText, iridescenceMaskMap, iridescenceMask);

            if (iridescenceThicknessMap.textureValue != null)
            {
                m_MaterialEditor.TexturePropertySingleLine(Styles.iridescenceThicknessMapText, iridescenceThicknessMap);
                // Display the remap of texture values.
                Vector2 remap = iridescenceThicknessRemap.vectorValue;
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.MinMaxSlider(Styles.iridescenceThicknessRemapText, ref remap.x, ref remap.y, 0.0f, 1.0f);
                if (EditorGUI.EndChangeCheck())
                {
                    iridescenceThicknessRemap.vectorValue = remap;
                }
            }
            else
            {
                // Allow the user to set the constant value of thickness if no thickness map is provided.
                m_MaterialEditor.TexturePropertySingleLine(Styles.iridescenceThicknessMapText, iridescenceThicknessMap, iridescenceThickness);
            }
        }

        protected void ShaderClearCoatInputGUI()
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.coatMaskText, coatMaskMap, coatMask);
        }

        protected void ShaderAnisoInputGUI()
        {
            if ((NormalMapSpace)normalMapSpace[0].floatValue == NormalMapSpace.TangentSpace)
            {
                m_MaterialEditor.TexturePropertySingleLine(Styles.tangentMapText, tangentMap);
            }
            else
            {
                m_MaterialEditor.TexturePropertySingleLine(Styles.tangentMapOSText, tangentMapOS);
            }
            m_MaterialEditor.ShaderProperty(anisotropy, Styles.anisotropyText);
            m_MaterialEditor.TexturePropertySingleLine(Styles.anisotropyMapText, anisotropyMap);
        }

        protected override void UpdateDisplacement()
        {
            for (int i = 0; i < m_LayerCount; ++i)
            {
                UpdateDisplacement(i);
            }
        }

        protected void UpdateDisplacement(int layerIndex)
        {
            DisplacementMode displaceMode = (DisplacementMode)displacementMode.floatValue;
            if (displaceMode == DisplacementMode.Pixel)
            {
                heightAmplitude[layerIndex].floatValue = heightPoMAmplitude[layerIndex].floatValue * 0.01f; // Conversion centimeters to meters.
                heightCenter[layerIndex].floatValue = 1.0f; // PoM is always inward so base (0 height) is mapped to 1 in the texture
            }
            else
            {
                HeightmapParametrization parametrization = (HeightmapParametrization)heightParametrization[layerIndex].floatValue;
                if (parametrization == HeightmapParametrization.MinMax)
                {
                    float offset = heightOffset[layerIndex].floatValue;
                    float amplitude = (heightMax[layerIndex].floatValue - heightMin[layerIndex].floatValue);

                    heightAmplitude[layerIndex].floatValue = amplitude * 0.01f; // Conversion centimeters to meters.
                    heightCenter[layerIndex].floatValue = -(heightMin[layerIndex].floatValue + offset) / Mathf.Max(1e-6f, amplitude);
                }
                else
                {
                    float amplitude = heightTessAmplitude[layerIndex].floatValue;
                    heightAmplitude[layerIndex].floatValue = amplitude * 0.01f;
                    heightCenter[layerIndex].floatValue = -heightOffset[layerIndex].floatValue / Mathf.Max(1e-6f, amplitude) + heightTessCenter[layerIndex].floatValue;
                }
            }
        }

        protected void DoLayerGUI(Material material, int layerIndex, bool isLayeredLit, bool showHeightMap, uint inputToggle = (uint)Expandable.Input, uint detailToggle = (uint)Expandable.Detail, Color colorDot = default(Color), bool subHeader = false)
        {
            UVBaseMapping uvBaseMapping = (UVBaseMapping)UVBase[layerIndex].floatValue;
            float X, Y, Z, W;

            using (var header = new HeaderScope(Styles.InputsText, inputToggle, this, colorDot: colorDot, subHeader: subHeader))
            {
                if (header.expanded)
                {
                    m_MaterialEditor.TexturePropertySingleLine(Styles.baseColorText, baseColorMap[layerIndex], baseColor[layerIndex]);

                    if ((MaterialId)materialID.floatValue == MaterialId.LitStandard ||
                        (MaterialId)materialID.floatValue == MaterialId.LitAniso ||
                        (MaterialId)materialID.floatValue == MaterialId.LitIridescence)
                    {
                        m_MaterialEditor.ShaderProperty(metallic[layerIndex], Styles.metallicText);
                    }

                    if (maskMap[layerIndex].textureValue == null)
                    {
                        m_MaterialEditor.ShaderProperty(smoothness[layerIndex], Styles.smoothnessText);
                    }
                    else
                    {
                        float remapMin = smoothnessRemapMin[layerIndex].floatValue;
                        float remapMax = smoothnessRemapMax[layerIndex].floatValue;
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.MinMaxSlider(Styles.smoothnessRemappingText, ref remapMin, ref remapMax, 0.0f, 1.0f);
                        if (EditorGUI.EndChangeCheck())
                        {
                            smoothnessRemapMin[layerIndex].floatValue = remapMin;
                            smoothnessRemapMax[layerIndex].floatValue = remapMax;
                        }

                        float aoMin = aoRemapMin[layerIndex].floatValue;
                        float aoMax = aoRemapMax[layerIndex].floatValue;
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.MinMaxSlider(Styles.aoRemappingText, ref aoMin, ref aoMax, 0.0f, 1.0f);
                        if (EditorGUI.EndChangeCheck())
                        {
                            aoRemapMin[layerIndex].floatValue = aoMin;
                            aoRemapMax[layerIndex].floatValue = aoMax;
                        }
                    }

                    m_MaterialEditor.TexturePropertySingleLine(((MaterialId)materialID.floatValue == MaterialId.LitSpecular) ? Styles.maskMapSpecularText : Styles.maskMapSText, maskMap[layerIndex]);

                    m_MaterialEditor.ShaderProperty(normalMapSpace[layerIndex], Styles.normalMapSpaceText);

                    // Triplanar only work with tangent space normal
                    if ((NormalMapSpace)normalMapSpace[layerIndex].floatValue == NormalMapSpace.ObjectSpace && ((UVBaseMapping)UVBase[layerIndex].floatValue == UVBaseMapping.Triplanar))
                    {
                        EditorGUILayout.HelpBox(Styles.normalMapSpaceWarning.text, MessageType.Error);
                    }

                    // We have two different property for object space and tangent space normal map to allow
                    // 1. to go back and forth
                    // 2. to avoid the warning that ask to fix the object normal map texture (normalOS are just linear RGB texture
                    if ((NormalMapSpace)normalMapSpace[layerIndex].floatValue == NormalMapSpace.TangentSpace)
                    {
                        m_MaterialEditor.TexturePropertySingleLine(Styles.normalMapText, normalMap[layerIndex], normalScale[layerIndex]);
                        m_MaterialEditor.TexturePropertySingleLine(Styles.bentNormalMapText, bentNormalMap[layerIndex]);
                    }
                    else
                    {
                        // No scaling in object space
                        m_MaterialEditor.TexturePropertySingleLine(Styles.normalMapOSText, normalMapOS[layerIndex]);
                        m_MaterialEditor.TexturePropertySingleLine(Styles.bentNormalMapOSText, bentNormalMapOS[layerIndex]);
                    }

                    DisplacementMode displaceMode = (DisplacementMode)displacementMode.floatValue;
                    if (displaceMode != DisplacementMode.None || showHeightMap)
                    {
                        EditorGUI.BeginChangeCheck();
                        m_MaterialEditor.TexturePropertySingleLine(Styles.heightMapText, heightMap[layerIndex]);
                        if (!heightMap[layerIndex].hasMixedValue && heightMap[layerIndex].textureValue != null && !displacementMode.hasMixedValue)
                        {
                            EditorGUI.indentLevel++;
                            if (displaceMode == DisplacementMode.Pixel)
                            {
                                m_MaterialEditor.ShaderProperty(heightPoMAmplitude[layerIndex], Styles.heightMapAmplitudeText);
                            }
                            else
                            {
                                m_MaterialEditor.ShaderProperty(heightParametrization[layerIndex], Styles.heightMapParametrization);
                                if (!heightParametrization[layerIndex].hasMixedValue)
                                {
                                    HeightmapParametrization parametrization = (HeightmapParametrization)heightParametrization[layerIndex].floatValue;
                                    if (parametrization == HeightmapParametrization.MinMax)
                                    {
                                        EditorGUI.BeginChangeCheck();
                                        m_MaterialEditor.ShaderProperty(heightMin[layerIndex], Styles.heightMapMinText);
                                        if (EditorGUI.EndChangeCheck())
                                            heightMin[layerIndex].floatValue = Mathf.Min(heightMin[layerIndex].floatValue, heightMax[layerIndex].floatValue);
                                        EditorGUI.BeginChangeCheck();
                                        m_MaterialEditor.ShaderProperty(heightMax[layerIndex], Styles.heightMapMaxText);
                                        if (EditorGUI.EndChangeCheck())
                                            heightMax[layerIndex].floatValue = Mathf.Max(heightMin[layerIndex].floatValue, heightMax[layerIndex].floatValue);
                                    }
                                    else
                                    {
                                        EditorGUI.BeginChangeCheck();
                                        m_MaterialEditor.ShaderProperty(heightTessAmplitude[layerIndex], Styles.heightMapAmplitudeText);
                                        if (EditorGUI.EndChangeCheck())
                                            heightTessAmplitude[layerIndex].floatValue = Mathf.Max(0f, heightTessAmplitude[layerIndex].floatValue);
                                        m_MaterialEditor.ShaderProperty(heightTessCenter[layerIndex], Styles.heightMapCenterText);
                                    }

                                    m_MaterialEditor.ShaderProperty(heightOffset[layerIndex], Styles.heightMapOffsetText);
                                }
                            }
                            EditorGUI.indentLevel--;
                        }
                        // UI only updates intermediate values, this will update the values actually used by the shader.
                        if (EditorGUI.EndChangeCheck())
                        {
                            UpdateDisplacement(layerIndex);
                        }
                    }

                    switch ((MaterialId)materialID.floatValue)
                    {
                        case MaterialId.LitSSS:
                        case MaterialId.LitTranslucent:
                            ShaderSSSAndTransmissionInputGUI(material, layerIndex);
                            break;
                        case MaterialId.LitStandard:
                            // Nothing
                            break;

                        // Following mode are not supported by layered lit and will not be call by it
                        // as the MaterialId enum don't define it
                        case MaterialId.LitAniso:
                            ShaderAnisoInputGUI();
                            break;
                        case MaterialId.LitSpecular:
                            ShaderSpecularColorInputGUI(material);
                            break;
                        case MaterialId.LitIridescence:
                            ShaderIridescenceInputGUI();
                            break;

                        default:
                            Debug.Assert(false, "Encountered an unsupported MaterialID.");
                            break;
                    }

                    if (!isLayeredLit)
                    {
                        ShaderClearCoatInputGUI();
                    }

                    EditorGUILayout.Space();

                    EditorGUI.BeginChangeCheck();
                    m_MaterialEditor.ShaderProperty(UVBase[layerIndex], Styles.UVBaseMappingText);
                    uvBaseMapping = (UVBaseMapping)UVBase[layerIndex].floatValue;

                    X = (uvBaseMapping == UVBaseMapping.UV0) ? 1.0f : 0.0f;
                    Y = (uvBaseMapping == UVBaseMapping.UV1) ? 1.0f : 0.0f;
                    Z = (uvBaseMapping == UVBaseMapping.UV2) ? 1.0f : 0.0f;
                    W = (uvBaseMapping == UVBaseMapping.UV3) ? 1.0f : 0.0f;

                    UVMappingMask[layerIndex].colorValue = new Color(X, Y, Z, W);

                    if ((uvBaseMapping == UVBaseMapping.Planar) || (uvBaseMapping == UVBaseMapping.Triplanar))
                    {
                        m_MaterialEditor.ShaderProperty(TexWorldScale[layerIndex], Styles.texWorldScaleText);
                    }
                    m_MaterialEditor.TextureScaleOffsetProperty(baseColorMap[layerIndex]);
                    if (EditorGUI.EndChangeCheck())
                    {
                        // Precompute.
                        InvTilingScale[layerIndex].floatValue = 2.0f / (Mathf.Abs(baseColorMap[layerIndex].textureScaleAndOffset.x) + Mathf.Abs(baseColorMap[layerIndex].textureScaleAndOffset.y));
                        if ((uvBaseMapping == UVBaseMapping.Planar) || (uvBaseMapping == UVBaseMapping.Triplanar))
                        {
                            InvTilingScale[layerIndex].floatValue = InvTilingScale[layerIndex].floatValue / TexWorldScale[layerIndex].floatValue;
                        }
                    }
                }
            }

            using (var header = new HeaderScope(Styles.detailText, detailToggle, this, colorDot: colorDot, subHeader: subHeader))
            {
                if (header.expanded)
                {
                    m_MaterialEditor.TexturePropertySingleLine(Styles.detailMapNormalText, detailMap[layerIndex]);

                    if (material.GetTexture(isLayeredLit ? kDetailMap + layerIndex : kDetailMap))
                    {
                        EditorGUI.indentLevel++;

                        // When Planar or Triplanar is enable the UVDetail use the same mode, so we disable the choice on UVDetail
                        if (uvBaseMapping == UVBaseMapping.Planar)
                        {
                            EditorGUILayout.LabelField(Styles.UVDetailMappingText.text + ": Planar");
                        }
                        else if (uvBaseMapping == UVBaseMapping.Triplanar)
                        {
                            EditorGUILayout.LabelField(Styles.UVDetailMappingText.text + ": Triplanar");
                        }
                        else
                        {
                            m_MaterialEditor.ShaderProperty(UVDetail[layerIndex], Styles.UVDetailMappingText);
                        }

                        // Setup the UVSet for detail, if planar/triplanar is use for base, it will override the mapping of detail (See shader code)
                        X = ((UVDetailMapping)UVDetail[layerIndex].floatValue == UVDetailMapping.UV0) ? 1.0f : 0.0f;
                        Y = ((UVDetailMapping)UVDetail[layerIndex].floatValue == UVDetailMapping.UV1) ? 1.0f : 0.0f;
                        Z = ((UVDetailMapping)UVDetail[layerIndex].floatValue == UVDetailMapping.UV2) ? 1.0f : 0.0f;
                        W = ((UVDetailMapping)UVDetail[layerIndex].floatValue == UVDetailMapping.UV3) ? 1.0f : 0.0f;
                        UVDetailsMappingMask[layerIndex].colorValue = new Color(X, Y, Z, W);

                        EditorGUI.indentLevel++;
                        m_MaterialEditor.ShaderProperty(linkDetailsWithBase[layerIndex], Styles.linkDetailsWithBaseText);
                        EditorGUI.indentLevel--;

                        m_MaterialEditor.TextureScaleOffsetProperty(detailMap[layerIndex]);
                        if ((DisplacementMode)displacementMode.floatValue == DisplacementMode.Pixel && (UVDetail[layerIndex].floatValue != UVBase[layerIndex].floatValue))
                        {
                            if (material.GetTexture(kDetailMap + m_PropertySuffixes[layerIndex]))
                                EditorGUILayout.HelpBox(Styles.perPixelDisplacementDetailsWarning.text, MessageType.Warning);
                        }
                        m_MaterialEditor.ShaderProperty(detailAlbedoScale[layerIndex], Styles.detailAlbedoScaleText);
                        m_MaterialEditor.ShaderProperty(detailNormalScale[layerIndex], Styles.detailNormalScaleText);
                        m_MaterialEditor.ShaderProperty(detailSmoothnessScale[layerIndex], Styles.detailSmoothnessScaleText);
                        EditorGUI.indentLevel--;
                    }
                }
            }

            var surfaceTypeValue = (SurfaceType)surfaceType.floatValue;
            if (surfaceTypeValue == SurfaceType.Transparent
                && refractionModel != null)
            {
                using (var header = new HeaderScope(StylesBaseUnlit.TransparencyInputsText, (uint)Expandable.Transparency, this))
                {
                    if (header.expanded)
                    {
                        var isPrepass = HDRenderQueue.k_RenderQueue_PreRefraction.Contains(material.renderQueue);
                        if (refractionModel != null
                            // Refraction is not available for pre-refraction objects
                            && !isPrepass)
                        {
                            m_MaterialEditor.ShaderProperty(refractionModel, Styles.refractionModelText);
                            var mode = (ScreenSpaceRefraction.RefractionModel)refractionModel.floatValue;
                            if (mode != ScreenSpaceRefraction.RefractionModel.None)
                            {
                                m_MaterialEditor.ShaderProperty(ior, Styles.refractionIorText);

                                blendMode.floatValue = (float)BlendMode.Alpha;

                                if (thicknessMap[0].textureValue == null)
                                    m_MaterialEditor.ShaderProperty(thickness[0], Styles.refractionThicknessText);
                                m_MaterialEditor.TexturePropertySingleLine(Styles.refractionThicknessMapText, thicknessMap[0]);

                                ++EditorGUI.indentLevel;
                                m_MaterialEditor.ShaderProperty(thicknessMultiplier, Styles.refractionThicknessMultiplierText);
                                thicknessMultiplier.floatValue = Mathf.Max(thicknessMultiplier.floatValue, 0);
                                --EditorGUI.indentLevel;

                                m_MaterialEditor.TexturePropertySingleLine(Styles.transmittanceColorText, transmittanceColorMap, transmittanceColor);
                                ++EditorGUI.indentLevel;
                                m_MaterialEditor.ShaderProperty(atDistance, Styles.atDistanceText);
                                atDistance.floatValue = Mathf.Max(atDistance.floatValue, 0);
                                --EditorGUI.indentLevel;
                            }
                        }

                        DoDistortionInputsGUI();
                    }
                }
            }
        }

        protected void DoEmissiveGUI(Material material)
        {
            using (var header = new HeaderScope(Styles.emissiveLabelText, (uint)Expandable.Emissive, this))
            {
                if (header.expanded)
                {
                    EditorGUI.BeginChangeCheck();
                    m_MaterialEditor.ShaderProperty(useEmissiveIntensity, Styles.useEmissiveIntensityText);
                    bool updateEmissiveColor = EditorGUI.EndChangeCheck();

                    if (useEmissiveIntensity.floatValue == 0)
                    {
                        EditorGUI.BeginChangeCheck();
                        DoEmissiveTextureProperty(material, emissiveColor);
                        if (EditorGUI.EndChangeCheck() || updateEmissiveColor)
                            emissiveColor.colorValue = emissiveColor.colorValue;
                        EditorGUILayout.HelpBox(Styles.emissiveIntensityFromHDRColorText.text, MessageType.Info, true);
                    }
                    else
                    {
                        EditorGUI.BeginChangeCheck();
                        {
                            DoEmissiveTextureProperty(material, emissiveColorLDR);
                            emissiveColorLDR.colorValue = NormalizeEmissionColor(ref updateEmissiveColor, emissiveColorLDR.colorValue);

                            using (new EditorGUILayout.HorizontalScope())
                            {
                                EmissiveIntensityUnit unit = (EmissiveIntensityUnit)emissiveIntensityUnit.floatValue;

                                if (unit == EmissiveIntensityUnit.Luminance)
                                    m_MaterialEditor.ShaderProperty(emissiveIntensity, Styles.emissiveIntensityText);
                                else
                                {
                                    float evValue = LightUtils.ConvertLuminanceToEv(emissiveIntensity.floatValue);
                                    evValue = EditorGUILayout.FloatField(Styles.emissiveIntensityText, evValue);
                                    emissiveIntensity.floatValue = LightUtils.ConvertEvToLuminance(evValue);
                                }
                                emissiveIntensityUnit.floatValue = (float)(EmissiveIntensityUnit)EditorGUILayout.EnumPopup(unit);
                            }
                        }
                        if (EditorGUI.EndChangeCheck() || updateEmissiveColor)
                            emissiveColor.colorValue = emissiveColorLDR.colorValue * emissiveIntensity.floatValue;
                    }

                    m_MaterialEditor.ShaderProperty(emissiveExposureWeight, Styles.emissiveExposureWeightText);

                    m_MaterialEditor.ShaderProperty(albedoAffectEmissive, Styles.albedoAffectEmissiveText);

                    DoEmissionArea(material);
                }
            }
        }

        protected void DoEmissiveTextureProperty(Material material, MaterialProperty color)
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.emissiveText, emissiveColorMap, color);

            if (material.GetTexture(kEmissiveColorMap))
            {
                EditorGUI.indentLevel++;
                m_MaterialEditor.ShaderProperty(UVEmissive, Styles.UVBaseMappingText);
                UVBaseMapping uvEmissiveMapping = (UVBaseMapping)UVEmissive.floatValue;

                float X, Y, Z, W;
                X = (uvEmissiveMapping == UVBaseMapping.UV0) ? 1.0f : 0.0f;
                Y = (uvEmissiveMapping == UVBaseMapping.UV1) ? 1.0f : 0.0f;
                Z = (uvEmissiveMapping == UVBaseMapping.UV2) ? 1.0f : 0.0f;
                W = (uvEmissiveMapping == UVBaseMapping.UV3) ? 1.0f : 0.0f;

                UVMappingMaskEmissive.colorValue = new Color(X, Y, Z, W);

                if ((uvEmissiveMapping == UVBaseMapping.Planar) || (uvEmissiveMapping == UVBaseMapping.Triplanar))
                {
                    m_MaterialEditor.ShaderProperty(TexWorldScaleEmissive, Styles.texWorldScaleText);
                }

                m_MaterialEditor.TextureScaleOffsetProperty(emissiveColorMap);
                EditorGUI.indentLevel--;
            }
        }

        protected override void MaterialPropertiesGUI(Material material)
        {
            DoLayerGUI(material, 0, false, false);
            DoEmissiveGUI(material);
            // The parent Base.ShaderPropertiesGUI will call DoEmissionArea
        }

        protected override void MaterialPropertiesAdvanceGUI(Material material)
        {
            m_MaterialEditor.ShaderProperty(enableSpecularOcclusion, Styles.enableSpecularOcclusionText);
        }

        protected override bool ShouldEmissionBeEnabled(Material material)
        {
            return (material.GetColor(kEmissiveColor) != Color.black) || material.GetTexture(kEmissiveColorMap);
        }

        protected override void SetupMaterialKeywordsAndPassInternal(Material material)
        {
            SetupMaterialKeywordsAndPass(material);
        }

        // All Setup Keyword functions must be static. It allow to create script to automatically update the shaders with a script if code change
        static public void SetupMaterialKeywordsAndPass(Material material)
        {
            SetupBaseLitKeywords(material);
            SetupBaseLitMaterialPass(material);

            NormalMapSpace normalMapSpace = (NormalMapSpace)material.GetFloat(kNormalMapSpace);

            // Note: keywords must be based on Material value not on MaterialProperty due to multi-edit & material animation
            // (MaterialProperty value might come from renderer material property block)
            CoreUtils.SetKeyword(material, "_MAPPING_PLANAR", ((UVBaseMapping)material.GetFloat(kUVBase)) == UVBaseMapping.Planar);
            CoreUtils.SetKeyword(material, "_MAPPING_TRIPLANAR", ((UVBaseMapping)material.GetFloat(kUVBase)) == UVBaseMapping.Triplanar);

            CoreUtils.SetKeyword(material, "_NORMALMAP_TANGENT_SPACE", (normalMapSpace == NormalMapSpace.TangentSpace));

            if (normalMapSpace == NormalMapSpace.TangentSpace)
            {
                // With details map, we always use a normal map and Unity provide a default (0, 0, 1) normal map for it
                CoreUtils.SetKeyword(material, "_NORMALMAP", material.GetTexture(kNormalMap) || material.GetTexture(kDetailMap));
                CoreUtils.SetKeyword(material, "_TANGENTMAP", material.GetTexture(kTangentMap));
                CoreUtils.SetKeyword(material, "_BENTNORMALMAP", material.GetTexture(kBentNormalMap));
            }
            else // Object space
            {
                // With details map, we always use a normal map but in case of objects space there is no good default, so the result will be weird until users fix it
                CoreUtils.SetKeyword(material, "_NORMALMAP", material.GetTexture(kNormalMapOS) || material.GetTexture(kDetailMap));
                CoreUtils.SetKeyword(material, "_TANGENTMAP", material.GetTexture(kTangentMapOS));
                CoreUtils.SetKeyword(material, "_BENTNORMALMAP", material.GetTexture(kBentNormalMapOS));
            }
            CoreUtils.SetKeyword(material, "_MASKMAP", material.GetTexture(kMaskMap));

            CoreUtils.SetKeyword(material, "_EMISSIVE_MAPPING_PLANAR", ((UVBaseMapping)material.GetFloat(kUVEmissive)) == UVBaseMapping.Planar && material.GetTexture(kEmissiveColorMap));
            CoreUtils.SetKeyword(material, "_EMISSIVE_MAPPING_TRIPLANAR", ((UVBaseMapping)material.GetFloat(kUVEmissive)) == UVBaseMapping.Triplanar && material.GetTexture(kEmissiveColorMap));
            CoreUtils.SetKeyword(material, "_EMISSIVE_COLOR_MAP", material.GetTexture(kEmissiveColorMap));

            CoreUtils.SetKeyword(material, "_ENABLESPECULAROCCLUSION", material.GetFloat(kEnableSpecularOcclusion) > 0.0f);
            CoreUtils.SetKeyword(material, "_HEIGHTMAP", material.GetTexture(kHeightMap));
            CoreUtils.SetKeyword(material, "_ANISOTROPYMAP", material.GetTexture(kAnisotropyMap));
            CoreUtils.SetKeyword(material, "_DETAIL_MAP", material.GetTexture(kDetailMap));
            CoreUtils.SetKeyword(material, "_SUBSURFACE_MASK_MAP", material.GetTexture(kSubsurfaceMaskMap));
            CoreUtils.SetKeyword(material, "_THICKNESSMAP", material.GetTexture(kThicknessMap));
            CoreUtils.SetKeyword(material, "_IRIDESCENCE_THICKNESSMAP", material.GetTexture(kIridescenceThicknessMap));
            CoreUtils.SetKeyword(material, "_SPECULARCOLORMAP", material.GetTexture(kSpecularColorMap));

            bool needUV2 = (UVDetailMapping)material.GetFloat(kUVDetail) == UVDetailMapping.UV2 || (UVBaseMapping)material.GetFloat(kUVBase) == UVBaseMapping.UV2;
            bool needUV3 = (UVDetailMapping)material.GetFloat(kUVDetail) == UVDetailMapping.UV3 || (UVBaseMapping)material.GetFloat(kUVBase) == UVBaseMapping.UV3;

            if (needUV3)
            {
                material.DisableKeyword("_REQUIRE_UV2");
                material.EnableKeyword("_REQUIRE_UV3");
            }
            else if (needUV2)
            {
                material.EnableKeyword("_REQUIRE_UV2");
                material.DisableKeyword("_REQUIRE_UV3");
            }
            else
            {
                material.DisableKeyword("_REQUIRE_UV2");
                material.DisableKeyword("_REQUIRE_UV3");
            }

            MaterialId materialId = (MaterialId)material.GetFloat(kMaterialID);
            CoreUtils.SetKeyword(material, "_MATERIAL_FEATURE_SUBSURFACE_SCATTERING", materialId == MaterialId.LitSSS);
            CoreUtils.SetKeyword(material, "_MATERIAL_FEATURE_TRANSMISSION", materialId == MaterialId.LitTranslucent || (materialId == MaterialId.LitSSS && material.GetFloat(kTransmissionEnable) > 0.0f));

            CoreUtils.SetKeyword(material, "_MATERIAL_FEATURE_ANISOTROPY", materialId == MaterialId.LitAniso);
            // No material Id for clear coat, just test the attribute
            CoreUtils.SetKeyword(material, "_MATERIAL_FEATURE_CLEAR_COAT", material.GetFloat(kCoatMask) > 0.0 || material.GetTexture(kCoatMaskMap));
            CoreUtils.SetKeyword(material, "_MATERIAL_FEATURE_IRIDESCENCE", materialId == MaterialId.LitIridescence);
            CoreUtils.SetKeyword(material, "_MATERIAL_FEATURE_SPECULAR_COLOR", materialId == MaterialId.LitSpecular);

            var refractionModelValue = (ScreenSpaceRefraction.RefractionModel)material.GetFloat(kRefractionModel);
            // We can't have refraction in pre-refraction queue
            var canHaveRefraction = !HDRenderQueue.k_RenderQueue_PreRefraction.Contains(material.renderQueue);
            CoreUtils.SetKeyword(material, "_REFRACTION_PLANE", (refractionModelValue == ScreenSpaceRefraction.RefractionModel.Box) && canHaveRefraction);
            CoreUtils.SetKeyword(material, "_REFRACTION_SPHERE", (refractionModelValue == ScreenSpaceRefraction.RefractionModel.Sphere) && canHaveRefraction);
            CoreUtils.SetKeyword(material, "_TRANSMITTANCECOLORMAP", material.GetTexture(kTransmittanceColorMap) && canHaveRefraction);
        }
    }
} // namespace UnityEditor