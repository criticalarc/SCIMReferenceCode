using Microsoft.SCIM;

namespace Microsoft.SystemForCrossDomainIdentityManagement.Tests;

public class Core2EnterpriseUserJsonDeserializingFactoryTests
{
    private const string Core2EnterpriseUserString = "{\n  \"active\" : true,\n  \"displayName\" : \"test user1\",\n  \"emails\" : [ {\n    \"type\" : \"work\",\n    \"primary\" : true,\n    \"value\" : \"test.user1@somedomain.com\"\n  } ],\n  \"locale\" : \"en-US\",\n  \"meta\" : {\n    \"resourceType\" : null,\n    \"created\" : \"2025-12-10T22:52:06.4081651\",\n    \"lastModified\" : \"2025-12-10T22:52:06.4439685\",\n    \"version\" : null,\n    \"location\" : \"\"\n  },\n  \"name\" : {\n    \"familyName\" : \"user123\",\n    \"givenName\" : \"test\"\n  },\n  \"userName\" : \"test.user1@somedomain.com\",\n  \"externalId\" : \"00uy542fqumJzNFq2697\",\n  \"id\" : \"z4dl6zdheglurdnuc4mwn54kcq@somedomain.local\",\n  \"schemas\" : [ \"urn:ietf:params:scim:schemas:core:2.0:User\" ],\n  \"groups\" : [ ]\n}";

    [Fact]
    public void Create()
    {
        var core2EnterpriseUserJsonDeserializingFactory = new Core2EnterpriseUserJsonDeserializingFactory();
        var core2EnterpriseUser = core2EnterpriseUserJsonDeserializingFactory.Create(Core2EnterpriseUserString);
        Assert.NotNull(core2EnterpriseUser);
    }
}