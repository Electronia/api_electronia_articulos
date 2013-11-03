<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="api.detocho.com.mx" generation="1" functional="0" release="0" Id="46c51024-bfb3-43f3-9fde-ff6c2b71da7d" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="api.detocho.com.mxGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="webApiRol:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/api.detocho.com.mx/api.detocho.com.mxGroup/LB:webApiRol:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="webApiRol:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/api.detocho.com.mx/api.detocho.com.mxGroup/MapwebApiRol:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="webApiRolInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/api.detocho.com.mx/api.detocho.com.mxGroup/MapwebApiRolInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:webApiRol:Endpoint1">
          <toPorts>
            <inPortMoniker name="/api.detocho.com.mx/api.detocho.com.mxGroup/webApiRol/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapwebApiRol:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/api.detocho.com.mx/api.detocho.com.mxGroup/webApiRol/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapwebApiRolInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/api.detocho.com.mx/api.detocho.com.mxGroup/webApiRolInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="webApiRol" generation="1" functional="0" release="0" software="D:\websites\api_electronia_articulos\api.detocho.com.mx\csx\Release\roles\webApiRol" entryPoint="base\x86\WaHostBootstrapper.exe" parameters="base\x86\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;webApiRol&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;webApiRol&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/api.detocho.com.mx/api.detocho.com.mxGroup/webApiRolInstances" />
            <sCSPolicyUpdateDomainMoniker name="/api.detocho.com.mx/api.detocho.com.mxGroup/webApiRolUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/api.detocho.com.mx/api.detocho.com.mxGroup/webApiRolFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="webApiRolUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="webApiRolFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="webApiRolInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="2411c56d-f8e1-4ec6-bc73-42152a97adad" ref="Microsoft.RedDog.Contract\ServiceContract\api.detocho.com.mxContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="c30ea0d9-b06a-45a5-b7e9-31696c52f9c7" ref="Microsoft.RedDog.Contract\Interface\webApiRol:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/api.detocho.com.mx/api.detocho.com.mxGroup/webApiRol:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>