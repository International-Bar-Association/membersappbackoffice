﻿DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.ScheduleEventLocations')
AND col_name(parent_object_id, parent_column_id) = 'conferenceRoomId';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[ScheduleEventLocations] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[ScheduleEventLocations] DROP COLUMN [conferenceRoomId]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201708111005229_Removes link to conf_rooms', N'IBA_Common.Migrations.Configuration',  0x1F8B0800000000000400ED3DDB6EE4BA91EF0BEC3F34FA693798B86DCF3983D9819DC0C71E678D8CC75E5F829C274356D3B670D45247527B6C2CF26579C827E51796D4957716294A2DCF0EE6C1D32259248BC562B1AA58F5AF7FFCF3E08F2FAB78F68CB23C4A93C3F9DECEEE7C8692305D46C9E3E17C533CFCFEE3FC8F7FF8F77F3BF8BC5CBDCCFED2D47B4FEAE196497E387F2A8AF5A7C5220F9FD02AC877565198A579FA50EC84E96A112CD3C5FEEEEE7F2DF6F6160883986358B3D9C1D52629A2152A7FE09FC76912A275B109E2F37489E2BCFE8E4BAE4BA8B3AFC10AE5EB204487F3B35F8EEE8ED3D52A4DE6B3A3380AF008AE51FC309F054992164181C7F7E93647D74596268FD76BFC21886F5ED708D77B08E21CD5E3FED455874E61779F4C61D1356C40859BBC48579600F7DED73859F0CD9D303B6F7186B1F61963B77825B32E3177383F5AAF2FF72FCF519E078F18037C8F9F8EE38CD4A6B1BB532DC50ED3F4DDACABF0AE25074C35E4DFBBD9F1262E36193A4CD0A6C882F8DDEC72731F47E19FD1EB4DFA1B4A0E934D1CD303C543C565CC07FCE9324BD7282B5EAFD0433DFCB3E57CB660DB2DF8866D33AA4D3DA7A4F8F0D37CF615771EDCC7A8A5036AFED7459AA13FA10465418196974151A02C2130508949A177AEAFE30C91764D8727F8C70D266F499F7A385728900031B50951F42CEB5CDFEE04C5483A66AED9D7E0397A2CB12400788E4274F10D236D3EBB427159277F8AD6D5866409E78EA97D9AA5ABAB34E60993AE74779D6EB2100FE72635D5BC09B24754C007DE6E03E3A0DB9AAA01377FF5836DFEDA0EF47F366843564832CECB4DFE54832D6BDD713BBC1BB0BE663BA666E486EACD44E9291C2C3A7EA3E5420C0DD8F020AAE15BE540EFF707E54067F741D7DD090AA35510CF679719FE5F7DB27F9CCFAEC380742E1B891EFA97202FAA35B85D2FF1F8FA328D5C4AD355998A5388A502ED4AAA78A0571752FD41A5D2BE6A1ABA3D3B69FAC4621A9634E7B3F3E0E50B4A1E8B27CC9E8317BCEED10B5A365FEA71DC2611164C71A322DB008E37D215E9A4EDAAA3B1EAB3E526209CB15C9EC1C75E6DB2E545D25BA0385A4735F1F91FB4B36060BDCFF9D355C30A9CF6B9AB68BC75B9B81B80CBCE675B8FC5035A4C0FBC89AEF170AA3D632D1763713A434B97C6448077698707BB4419F9FF1878C15D99571CBADB6F9EB2F2D62213525B02AB2A358270CEC8A7AA4AA268AAACD9EB94E7C1BAF280AAF5D63941358C7EFCA083319AFC9A444451E3489C3A60E53D178FF00422B7CA01E12B76B48EF0443C0DAC85673536D37D562E54F7DC83FCD16BDEAD6E7B90BB755AEE41AEF55B95BEDF88968AA0FB1A0F699333A235B50A4DA925E0AAD9680242D99B0B5B380DA2182DAF5091BD1EA79BA4F07594723A9C21D53EC2BE8669892C66729BA3CCA069A3AADC35BA2E46D32656108402792D994060BEB2C050DED4D5E0BABEA018915CD7B3C5AE2D910029C4863C0CB40166FC3CA1585A26A8B63F98FEA04CFF7B334DD04C436D9B50D492E9FB55557D5A278E57CDE978C7F6C84873EA5A02F7D454B565A11A638523AB3720993B109CD84F377F3BD6D3B5FBC176A47D3597045EFFDA21CE49077B1315F1F092E10D7A2906EFE4368B07EFA3BD68E6C3E30C134E2CF6E7764F96DC2E3AC271BC5CF83A08F1BA5D24F16B03E79714F3A620B19F224A96FDAEFDC20930C481C1F360C0D9021D7FA59E310DBBA9251D6D55A81B645DA3D72171761F608864AEC043A26BF0264F07098FD8DBFF08E211969BE034CAF26214AD37B1608FD2D13926902B94A3E232C8F36F69B6ECCB283EAF82487752ECFFFCC1C3B0CB5E8ED3E421CA56A8F7989BB9FF77903F8D60D0083719DEB1F85C58AD07EFEDF2294DD0D7CDEA9E7083F1FAF2B63437DFD2D320C4C2DDE784B4EA0DEF4B1AFE966E8ACFD569765B84B697BD168097E11C8521E6FC9582CE4537C71FF6B9D12A07DD7FCE5AC1E3388856F2E3BD3B68EE9A5ADD3929140AE7A458C3F60CFF923E4689716C4D2DE9D8AA42DDD8EA1AB66323708C43AB2B49475696E9065655E8275BD4F72902AF5C039D8871DE3A001FE5EBAFA8D8695AEF54704F330C13F3DDDF7604B0EF66E0C69D9CB20F9553DEEFDD3FBCFFF8F38760F9FEC34FE8FDCFDFA3EF1241E53664A472F9987BF450C74DD9D35F8278E3BB2BA7DD50EE7AFFBBA1043BFDDD500E137F7E8ECADBCFC2DCA2A98CC183EA37F46CBBE7B8918DBD1D98698EDDF9383CC069BB90B3C8FF6E2150A7BF59E4A42CAD4A26E442F5DBE2FECD78274271F81A52E0A5FF12DD6741F66AA93367DABE49CDC8F07AF3B355F088C6500D8FA349AF1E200C2FB954A4255A1B189273B2388CB116A72820C4DEFBF2AB5471DB6BDB2BC439AADA6FD7711A2CD1F2266D7C257A5EEA9FF17D9ED427D734B3D21CCCCD3E3F13E248C372E79E6E9290FCFD1225BFD9313625982DF138E5785C989F16D85877AF87BA5F9F4F9C343A8DB07E862B511D90F7C8CB4D8C18B4DC31BF0872189502B48D609D0737ECE5FF2CEDC56E0748416C89FAA56371A17C25A0B1A89EE87C51869210F03EC300097798A1BFFA00F26B3F20A7719A669620943B55B6EDC6DEB3BC7AD07AB3F7BAF9F9BFF5BD8D1BDF5B31706EDB7640AEAD0A1D38B5DE7775354A0B2E948A7A70B18A1D496F561441D3EF32CFF2D33878ECE275D83FBF25A53E8E1FBC0E4B94C5AF78DD688EC522FF1C11035D23AA26CB2C8D30A5957AD4C3F9AEB0564CF5B3346FABEE8988AB50A4419BC4E7BE0FF60470DB41E2254A96E5568121F19AABBEA7AF5E1907DBDAFB46E0455BF7BDBEEED7B47E1F5C57FFC961457907B93ECBC9C2DACE5A623A4A964106DE1157585CFA46047BD862DEAE1F337CD584AE66790E13EF04D5925AAE918F4DC743DBCE3A9D64C143015DA48B4D719FBE40D7E824CAF1D177BF29E0BBEE280B9F2ACF71D8CE3BFA1644059E2C6105FDB69F4463D46B7105785B3A97B2220A63045DE0D3285E4197F7325D86415EA896D606ED5EB6130D6A4BC80E0B4CBC505CF3A79D01DD670907DEB495EAAD51E9CD6CB8DE519EA76154229C91D298F7126CCF9F93E5CC1CC6A1D33336FAC173BC10D11AA31E4B8A87F3DF0933D2C26DD5263CDCFA45070B7C77674790B4A899EA11A08E69A51A2F20C055376CEE351F1C2BE6E058232047FDD458356CC0BBE36ED8E23B7DD3D8817D4950045E0567DA69C0C356547897E79D668478601064F42317FD5B56E53A021FB652E3171E97C391048D9E065C8FBECC47F56A4DB3C6C6276CCCD099C79856B4647AFF360203D2BC76500D1CF2F441F608C592F9409EE439AE83057E447751D57835BEA3DD30E97711EC10F758C018F445523DEA9C1D855584D4E3200FCB8B1BAFC6C383808F4A823C8947A5898EAF50A96727A139B0F4882F2B419414A212324AC2681DC42614710D81FA4B32F5B60BBEE404ADC93B96A4302102D2B7D25B66D176C32D89093F6E24587B0503169B77119E0609726EC90A12ACDD188726411645E392208B88B7448295F73760AD3957F0691020EB7EAEA0BFCA423434F931F81997FA182C4C9EF8C4978D0091817FE608114874D29AFA7D2488B27D0AB1752C00A05CC90706808A4C06E9CC1069C6F926D1E732546B46C0F7123EE4CC40D71F2E628D5195E3F786E8723D1CF16EA8BF180E742B047B37A8A661EFEAD0CD49E13B64B5FBECDDA3BAFE351E7CDE2E4DA2A15C79366BACE6E2E92C3B9987121AD4B6FA2D880D4A2C8D2138283101E95CFE7A6010D1A1F29A20C60CDCA253FE9787F2C97D69E420414A04CB089E536D1CC96BEF119E2808DC6B54C8B492F97CD6796BC8F559C23666C151AA1D193046F30302A5866204C086BB10A058CC8A8254298FF5F01AFDB4012A7F7E48810AE79301A810E643B6A08CA86400D8098B3260B4000A40A20C042D681A4008BA1D293C51016401B679ABAC055B5FEA2DC0D6EF8CB5502B9E6B5A0EDAA01CC9D7847BA4638028738414606A0E5A1BF032D00A19C200D684502932295ECBF31936081C55511DB49E3F0440C6CF761E1483138E1390B95380D4705D5E3060670DC08826738F8818A049D4D2284A4D8E3FA134D8329B4187439A26D0B28834A0A9D4D2584A4D4E7662691067368ECA61FBA7B6F63431519AF422686142F54161FC6D6F38249902FF4A48CCC2BCEA6260A5E62A116474B40633A982D7C78DE494F13FA57407B2BBDA5A5ED919EA08C6D6D63A1C97D3059413310735C8DA9A64A9F931E2A9066B000B2C7C3D1C102709D523E2CB60A0059A68392EA4C78BDAA64A8191C9DD3E31D208DD5A8CC8EC85408BA12B4638139F0223CDE87D62A4166FB5089158AF60F62B5774B006270536EA91FBE4344D0C482D839199538006957EEC8433A19890DBFFB86AE2271B4F2A9971C5C2BCE2E77CE2AC293662830F39A9D11501042499C9C5CAE8E24F24E2CC2CE64BA30F5459CA936EC2E4A092A45E8CF47CA2C39F958AC87433DAF433DB5098E075321A1C5B5B6754DDD4B8E87F484A1E2E4A8E4983D5066AB7A166233DD8A066164F8765F39AB2B50BB465078B2A557AFDE160A1C8A97E701EACD751F248E558AFBFCCAEAB04EBC7BFBFB6CF40BEAA602C4206CFBC15A3EDA94833BC0BB952A2DC5CA23226F0495004F70179FA71BC5C09D5782B884253D8F426357488ABD828129B66E4FF35C9758F4CB497E80E97A7787A2B62752A4300A8AEB262FB19C9781FC4412679BB7C9CC69B55A2B682A95BB771616810ED47389C2A07080DE44A62E7D043683282B0509AAF70486D8E101A50FB518473B0E0964630E1091420D85759BA02511D73FD76A4398DC2014071DAD6C3D05B9D8C9A01507D82C310534ED3E0C4D289AD78CFC5765EE7B19698CEE4CCEEC0EEBB2DB4EAB5A508ADFA0E8746256AA681519FE1B0A854CC342CEA331C169D8C9906467F9F0C191B9475205AEE7552BA1C936C466366F13599927510DB0B110D4C61F1D6C1E9B211D380BAAF56071F9D9F983BFEE822BB835D04D77DB59A679BC8989B69FBDD161ABF96DDD729EE96C6DCD67BCFD45E247D768E0A8479FF741980E5BB48956558075DC8F1CB1C3F7CA1235C36A5AEB207B69A755F4C466049274CB93B74E364143527B32F447D8EEBB6E0FDA01CB68511C4B46F5F74DA5D5EAE69BE5BB05636D72EC35FD9225B9822B1D2DFE1D0C42CBB344CB17432446F367542550DB49F9E93AE410B60DAE4FE43D9604D78B4F1CA91E8287F4E7B82D3351E86D81A6944B839320570783755546C1A52FDC90246E998CD8028BF585C3A49CC69E6BA493E58117E9B869123FDF6BBC57CF8CC8ECCD4F842DBD34276526C83DDB4691D39BC571FEDAE2C9213B0FD3A1966A1328A80D804E5B36DCF26748D876113549E414686E83EDB29264550DD570BE625A60A645898580C875D670DA4E1D59F2C615089E7046054998518CBE406640459A6C466CB310900D97DC714598C924EF3C70C922E7082A7C0A8BC86059B1612FB317C5A28B5A07831C51F43FA62B1036CC998F9320B4DAB98059051B88AC51607452ED36F755F27C3E0254E7BAEDC5E7851E3C0F4CD3086E1FDCD737F7EB52C6F355D163646D0E83E5BC2AAF3AC09C0EAEF93A422A507A63515550FA8FA519102869AD33079CB5846A34DB6A686C924236398B92E199B1A9E1DAD4E8022542EA8D604513ED7EA470F7210C3B285E6413073CB523C12DE9E5A807F32E8AA1B60DF153AE8070C00863900BA945A0C8CF6EBB8DA81262316CB2DAA6F1647089DF08A3943E882F1340E5D062BE67ED57E1DFF3EEF47BB2066B262BD00F8520B1155CC69C588A862F164F809C84D15C451D4AF8AED998B052CE5855797EB8AB9FDC2336CE9FAA3D35AD1E0E9EFD35C75E715973FF6B65F6D201C257750E57662D8052C9394AE1F368513C7A8A9120B88A84EE5C4006B3EDAC2F95506E7572BCE5F257462D87EF5693294EB4548ED29A0DA09A7FD041E513BB12DCD04EBC72EF39364DFDE6A3C22D98A30E747E29A2FF534943D9A1591035A1A1A923C34103508A7F129832339938E764C98312DA332FED5594EF24EB54918E0B3E61F3058D3C6913A0085D92B5F4F295A077CC98228414F8B6ED4C3FC7F483E9A501C209F3BAA3EDC2B55B228C6881DC04531B9E5D5D021DE799664658C0B623D033BC7DCFEA406C68D67A6D5BA4F0118565BD72FB3E29E42F65EAA065E8F25830E798A4C0B32FDFEACCB10E705E41CC937B2F58494310250D016C7F56261F9605BA0103693A22E130E7CB02765CC1B80FFA151AAD2B81ACAB7BD2188CD64E42A43189F4951D1C082952EFA8FC19D506CD09B86CC81821CD7C6232732871D9A14FD0CC87F24F15C4C24D3D483FB99EA57808DD4E288F81A885FDA6003D24C8A2494F3F57D2235616D20875153D783642385DC73253898FECF2136A8CEA4E8C53877FF3272637D8309C74DED21A462366E4F2F11661039980D583429BA51CEDA3FBDD85FA886BE4D395FA5208A15BF17764044A749D115100FBDA90C1E0E0A6E18953677367C4B16D3368814706541E6D5B63BB09DD592526D43570127378C97407F42B7C5A799E28568537C95D67E587F697FB7D1A6EA484F4C08AA12B124A05489D0BC8E3AC5877EAAAACC678D2B255ECFD7BC40AB1D5261E7FA6FF1714C9E1C7515CE83247A407951A5EE9EEFEFEEEDCF67477114E4554CB03AA8D5273E650628CAD5DE7B12E50A2D570BBEB97DAC2C0225CF97B124521695125DA3FB3DF8331248A4211D5DA6948305DFF040C292AB60FAF7D16344B05B729B3F21BCF8C403EB32280A94259D5D7B3E23F4481C955A9A5C687B685DB9AA6E481895A28C316109A77A0CAA0652A63E31C2689E82F683D3BE04ED0B8655DE54D0CA5510D2BC9CE14BD9CBE1FC7FCBA69F66677FBD635BBF9B5D64784F7C9AEDCEFE6E3B0EF97169371A190CF89868BF03ED2651E7401E618B0CB23FEA40593525A1305A0531E171C443B464567B1F71A7F85CC2C5FBD6D0C5B85936246BB92CDFC98AD021ADAA6E92E7200B9F82EC3F56C1CB7FBAEDF2CA41971AB5E5A0A860561EC64485B3EAC996E95856DE9035364B04D379BF035A7F1D3151BD923BFBA67FE6AADC733DBB8057BD4F5C3AD2555F3120F002878E71E507577528247736A1F17EB0DB4A4A4083EE2A59FC2ADBBDA5F6F780EF3029D6BC9FFC7C042CF765D784BAEA47E2B2F056BD87A90862358864A257D6FDB858B1E1A4DC97968B21E5831D5241A4FA11B1183ACA7D9EA2DB06BBCA00D6CA83E8737593185F5CC6C3C2E833204AABEF22BBF5EC5DA9FBB51B8B02CC00479FCE07E20773FAA1F51941EBE3C4335C98057853A8DC3DDEF28660E293F590EFABE7C71E8ED72A46990F5D027939EC010E1D54CCC7FCF840657D659B3E107CB1C3363E5943AC0E93692393F9B8FC52DCA3592D10FF685BB3ECE33C78F98292C7E2E970BEB7FFD1133B51851F1B839D7448514F0DB46C54E8320F9BA38B5EE60198246E993B79D641CB34D8DBFFF983ED08F9E065EEC36343967951FA3001CB3C40640296F985E705856264327758925064FDB81A1F82CC7D68929863EEE747176DCC96D7362DCBCFEF6667F96D12FD6D830B6E30363896CBED2CC9C8E03C571FFDEBCDDA869A88410E475ED5147ADEC1C48A2E1A99878D4E8723B306E7441992885E30CA3004F092E9877501BA24F5E5A1A1CCF4C78DCCB314C0CCC233EC6D92B613F588211660C4A38AFA25A92A0FED65A68269B18966160EA3A99A8EB1B2BA58616FF9E2DFC51DF371A9F576FD6F4290F93837E810643D641C4F38EA8290B90B6F80FB3ADC84D10B2942C4B11E12A918626C10931BD03F19B6A92D7C6ACDBB5D0B6C40A9918EEF3594A397D119D84ECF6C823780DA1710E20B4633607F6813BD4070E9DD72C2C409EBA1726C4284F505F16B1F1075683028006B39D055069C8856CFED4E4F263DFC9D9E7A2CC0DA76D53EB85D666C21DA409DA69BF97EBE898B681D4721EEFD70BEBBB3B327E08B872801C6C3F99D00042F3BD951C4F504EFAEBCC882488C7C7699454918AD83583203AE2E90A2086A5BA87CC9095A13157852B0D383F4648E78D002E728DC8408E685889E16D411B8C6230949A8941A2657F23D10883634D074E9441D6A4BB5AA823720BDB062A115C528C865705A0124F9747169B422204BEAB18A31363AC391FB07F458E849B31ACBA5333DFF1D90E7405F208FC179F441B23CAFB489F1F06EA80C550A85835091FD6AF6E538E61CAE629F90D860E3B01B5530AC51051CDA3B902346A6E83B1171F40950272AE368825EA956967271A35795FEFCA628459B46740A84B27586D27921DDC99273516B49F92BD1EB487F66D7704F08FC709154CEAAB3A3B07AF17E1CE461B014F511240881721C820D9F198E583A086569334F7AA72C48E6330B8BE2C88425CBD73571C2AA5C00548455977E6784A54A86365DC292645498385D958A6115595585DF19552912564C8CA8C4688FDEA8CA2022598B5C6F92222C05316D70CBF1AF59759847EF52EEDBB89DDBCAB45BBA9E9B235D6E43BB230D41E2603E7A03A462612BDA068968C3596E8334005A3FDFFAE22990C9B6B4C64E446311BD72041202076754ADBFDC97852602450D2B2A537B5AD13D696A0D4277D0947FCE8E3B56D468937252E6686E1FB3728CAB19E5FE527A0C6B2E67B4A78CEC4224BB0C7DCF17347D7EBF695CD15429A50720AECF6CC4CFD60B8D8BCE29D0571D8E5666B599CF3A8F22B9EDAC8AF579385FDEA798062AD724AE8AC0F4D80E19A38AD01D9BBB44D2195541C80FACE84BD98DAE0733702DDA4C38B34098E839A1E9ADA9A2EFB3A965EA591077C49E852AD29EB95A66E4F2B742195D32151484C9263231744A6B14840EE94259675DB9B9235A15227444174A6795AFBFA2A23A3B8C1D89BA7EB13FB18EBEDBDA0A62D377AD0ED6F65DD7D1F75D2BCA6DFAAE8E246DD755157DCF9526D54842EC631809197115A4A444D78900BD6A044561009ABAB2B128AB9B47A51094851129EAC94623AD0A2707032998C8404E026C6C6EB5C7ED8CAAC81C436A1F058922843938A98CBAF57741EE1015294253511264A70498AE26ADAB386BA007AAD7C94BC51C49E6C6FEA8D0A42815510174B254683C587980CB7A2A3FE395BA13290CEF94D1C235518542FFD167F0DBA20743CA47095158F8BFF9989441FF45235728F4411FCADC855222013977F9E61D1259549609AF3F3A7469F8447440FD9794E6346A2AF4E7892043F4BC91E0C0E09EA3B44C5203A73F6BA6AE14A62B2062A94F043482AF16013237928110C048F40202EA529F08A8852FEDFC25DE0E034D9F962485D957853E7941935F51CB026466798FD3B7E61BFD4F832679A0F12090999FFDB0ADAD1E8ECA8C7800A1416E6B74B90F4C0B0596B2134870EA274B8E8D12780232113F6EF63188858C9AB3A2860685460586217F9A87934634CCC8CE1A83F94663C0919C15B273629413474817D6961D2C2AF547FD01FF14D2821D2CAE3609893A51FD3A4179F4D8812019D0121432C68AB6CE59F2903636136E444D1521FA6A112C832238CA8AE821080B5C4CA2B345C9E37C5646BC223102EFD1F22CB9D814EB4D81A78C56F731132A8AD85E74FD1F2C84311F5CAC4BCD928F29E061462450C745F2CB268A97EDB84F252FBA15208851A78E5440D6B220110B1E5F5B485FD30408A8465F6B8BBA41AB758C81E517C975F08C5CC686C9EF0B7A0CC2D72E76960A88792158B41F9C44C16316ACF21A46D71EFFC434BC5CBDFCE1FF009F9EC26B1A370100 , N'6.1.3-40302')
