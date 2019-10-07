﻿IF object_id(N'[dbo].[FK_dbo.PushMessageQueues_dbo.AppP2PMessage_AppP2PMessage_Id]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[PushMessageQueues] DROP CONSTRAINT [FK_dbo.PushMessageQueues_dbo.AppP2PMessage_AppP2PMessage_Id]
IF object_id(N'[dbo].[FK_dbo.PushMessageQueues_dbo.AppUserMessages_AppUserMessage_Id]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[PushMessageQueues] DROP CONSTRAINT [FK_dbo.PushMessageQueues_dbo.AppUserMessages_AppUserMessage_Id]
IF object_id(N'[dbo].[FK_dbo.PushMessageQueues_dbo.Devices_Device_Id]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[PushMessageQueues] DROP CONSTRAINT [FK_dbo.PushMessageQueues_dbo.Devices_Device_Id]
IF object_id(N'[dbo].[FK_dbo.PushMessageQueues_dbo.P2PMessage_P2PMessage_P2PMessageId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[PushMessageQueues] DROP CONSTRAINT [FK_dbo.PushMessageQueues_dbo.P2PMessage_P2PMessage_P2PMessageId]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_AppP2PMessage_Id' AND object_id = object_id(N'[dbo].[PushMessageQueues]', N'U'))
    DROP INDEX [IX_AppP2PMessage_Id] ON [dbo].[PushMessageQueues]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_AppUserMessage_Id' AND object_id = object_id(N'[dbo].[PushMessageQueues]', N'U'))
    DROP INDEX [IX_AppUserMessage_Id] ON [dbo].[PushMessageQueues]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_Device_Id' AND object_id = object_id(N'[dbo].[PushMessageQueues]', N'U'))
    DROP INDEX [IX_Device_Id] ON [dbo].[PushMessageQueues]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_P2PMessage_P2PMessageId' AND object_id = object_id(N'[dbo].[PushMessageQueues]', N'U'))
    DROP INDEX [IX_P2PMessage_P2PMessageId] ON [dbo].[PushMessageQueues]
ALTER TABLE [dbo].[DeviceOwners] ADD [NamedUserId] [nvarchar](max)
DROP TABLE [dbo].[PushMessageQueues]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201807031031298_Message Queue removed in place of Urban Airship integration', N'IBA_Common.Migrations.Configuration',  0x1F8B0800000000000400ED1DDB6EDDB8F1BD40FF41384F6D91F589EDDD451AD8BBF0DA716B348E0DDB59EC3E05B4441F0BAB239D95741C1B45BFAC0FFDA4FE42495D79BF8992E5340890E4F032248733C3E16866F8DF7FFFE7E0C7C775123CC0BC88B3F470B1BBF37A11C034CCA2385D1D2EB6E5DD376F163FFEF0C73F1CBC8BD68FC1CF6DBB7DDC0EF54C8BC3C57D596EDE2E9745780FD7A0D859C7619E15D95DB91366EB2588B2E5DEEBD77F5DEEEE2E2102B140B082E0E06A9B96F11A563FD0CFE32C0DE1A6DC82E43C8B605234E5A8E6BA821A7C006B586C40080F17673F1D7D3ACED6EB2C5D0447490CD00CAE6172B708409A662528D1FCDE7E2CE0759967E9EA7A830A4072F3B481A8DD1D480AD8CCFB6DDFDC7409AFF7F012967DC71654B82DCA6C6D097077BFC1C992EDEE84D945873384B57708BBE5135E7585B9C3C5D16673B977790E8B02AC1006D811DF1E27396E4D6277A7DE8A1DAAEBABA06FF0AA23074435F8CFABE0789B94DB1C1EA6705BE62079155C6E6F9338FC077CBAC97E83E961BA4D1272A268AAA88E2A40459779B68179F97405EF9AE99F458B6049F75BB21DBB6E449F664D69F9FDB78BE0031A1CDC26B0A30362FDD76596C3BFC114E6A084D125284B98A71806AC30C98DCE8C759C43DCAF1DF004FDB841E42D18530DE70A0201105D9F10C60FA2C1D5FD4E6002857366BA7D000FF1AAC21207E0210EE1C56784B445700593AA4D711F6F6A86A409E713D5FA34CFD65759C21226D9E8D375B6CD43349D9B4CD7F206E42B589A4FBC6303EDA4BB96B209B7FFAA27DBFE2B9AE8C1B26756250B5308B46160A2E34B65DFFDBD51D917FF1DA13323EF0745C7073A0117C139787C0FD355798FC8063C2242881F61D4963413F998C6E8C0449DCA7CAB65BBB35BD08F7202C3780D92457099A3FF3547F09B45701D02BC50D1AAD5D0DF83A2ACF7FBE32642B818CADD859049EA3A194BF3B51DE1B7EC2168D272D000DE70618BAF1C211CABA1A18F6727A333443D141EA41BAAA7B1BAD892092EB7C57DB53DA3CFBD66B2E8221D7CF21F6DE286F8FC4FDAF904B7E673F618548802273E77D5619F5D81ED27E0C2F974EFA9644087E99199E81A4DA7E6196B0516E9BD398C5C3A634DDBA51F9A6C0473FCFF29F012912A896CC74DB9FDE63EAFAE170246EF09AC6ED46AAC05C9EFD246DCF12E6F39E89467C1BACA80BAF7B34B827A1AC3E4410F632AA97096C6D8A2E2489C2A60D58514CDF0C4446F15034277E17813A385789A5807CF6A6EBA8BA758A91EC883ECD1ABE756271E44175B7C5172B524117D5FAAE6FDD594F42CA624827214B624492B917D46D6D4A735E9785DB443D023524C2D6FC59DAC8AA6838ED61EAE1D4BF7FDBEB2B370AC5606B3D7DB1E714E57DC9BB84CC6D7406FE06339FE1D3A4F461FA33BC78BF171860827E1C7735343AE11116E0B09E1B49596307D1D3068DF2ED2E4A985F35386041448ED978874BF615A152759C710C4EC016220B34DE75F6BBFBA69B7AD84B3AD2B55936C5A0CD2FDCE6E018288D76A7848F41D5EE4E9209011BB7B6F8C648425139CC679514E6254C01F082619E81C11C8152C6079098AE27396474305C5BB35885527C5DE77DF7B987635CA7196DEC5F91A0E9E73BBF6BF83E27E027B51B8CD11C7A27361BD197DB4CBFB2C851FB6EB5B2C0DA61BCBDBD6DC7CCE4E418894BB7729EE3518DEFB2CFC2DDB96EFEAD3EC6319DA5EA23A005EA673148648F29F226286D171B64DCB618A0896E11AB161CA7FCEF6CBE304C46BF1F1DE1F349FDA56FD39C95572E724DFC2F60C7F9FADE2543BB7B695706E75A56A6E4D0BDBB96138DAA9358D8433ABEA5413AB1B0CD32D9AFB148657ED814AC538EF1CA18E8ACD0758EEB4BD776AB8A7398289E4EE6F3B1CD8578171E75E4FD933D553F6776FEFF6DF7CF73D88F6BFFF16EE7FF7257E1AD6FA498CA42355DB47DDA3C73A6EAA917E06C9D6F7504EDC5071BD7F6EA8C0CE9F1BAA69A2E287B8BAFD2CF53DDAC608BC51FB969E6D798E99D9D4EC402D73EAC1A791014EEC82CF22FFDC82A1CE9F59C4A42C6C8A17E442F5CF25FDDBF9CE84E2D035A4445BFF3EBECD41FE646933A7FABE48CBC8F876F3B33558C1294CC3D358D26BFFCEF135979AB4F8AF0D14C9397D7198622F4E21C0C43EF8F22B3571DB5BDB6BC4399ADA3F6E920C4430BAC95A07D58197FA07749FC7EDF1354D6F3437966638C826DA26F0DD0326922C6CA2506C849A10C433C936E15C5C049E14D054772D6CED82394C4303C73F0D2434600E7FF101E4D761404E932CCB878168B7620443B6B5CEE95FDF7C19BAE64BF9B4F2DC564BAC304BAC6FC47E7F6A9A11F637AE96B7C0F14DEC6C70DB35174351EB0667C56902567DC4A47D5C05AEF521FED13EA04B6EF284F68D941834F2CF21FE34D01E9269946731A2B4CA8273B878CDED15D5FC2C2BBAA6BB3CE26A1429D0C63A720C411D0DEB79D087549D3402B931FEAED0E1F61987308971C836FFB859E54825EA9AEFA99B57272FFE8AD675D81FB447AD22E767976A68CFB34F2739B82B4D37E9625BDE668FA67B7412174850DE6E2B15DA6C9F8EF2F0BEF61C146F13D7FE33884BB458EC96D0F5F9D6656B05379B419BCBC17B26299697719840D30D3E8D93B5E9F65E6651088A52B6B53668F7C24E24A86742765822E235C5F525A2D94A8D3043F759CA80D7B152C31AF5FDCE46EA1D154516C615C2A9339DF297A5477E9746813E9AABBF0FB7F7D873B411F106A11EE915878BBF702B52C2ED9C5A59B88D472F0DFCF5CE0E772E132B5523401E832E9BAF41407A3F6D26AD823956F4C1EC1320471E71209BB641F8413F6D3E5C473777C3B1042832DE0567DA69C19BED289717C03BCDB039058C90319497644EF88A296B3DF229CC50B12556A8D1B9F34FC04F0A2753D9C44D3C4E45BEBF96BC641261E0B80F16F8E1BD7464F355B8ECF4D324DD51E929EED28011E88BB48E5109F0598CB32C1C8322ACEE21AC0D034DC27C5602E4091C5974747C052B231F0E3843CA10D2BD419C96BC05264EC37803121D8A988E86C61BBCF46E08B6E6046EB0FB705AEA106132B6F423E5B21B86D9121D7EDC48B071C632D86CD6336B1E24C878834948B0F11E199B0469144D4B8234225E1209D64E77067BCD78E0CD830069AF3F09FDD5E6F1B1C98FC2CFB4D4476161F6C4C7079418A80C6C74898942A2D2D6E4612946943D48471318DDA5A4AEB0C0F3C42E22F4B178506EF77F062E9462690A3E9462C26470B10FD4289C587F81C1A62ED4A3370D55347E725B99C070A8256737436B6A4C6745F3258A250A0CF71A96A23B6BB108FA2F3FE2DB27C7A33438E2A62402465DA48C40C9A16801D0417B1C148B5511906AD3821A5E6BBDD040E5E20A45B8A7EE551A80BD9814012345AFC17A45204811AB01C1DD6A84F0F8AB8F05D836384209B651672DC036810D4AA8B578D46D07F9652016EF09E315A88148B99588E049FC723460752B16AE96905B2CCFD2E9168886F22C61AC4035323377EB208405279A8D0CCB1CA45682B1872CBD6A038C28729AF28831343E5B9A9F89C5B1D25E812DBDC1793CA42932DBF04833344A5B9AA589C589A4BF02717A33B418B67F6AEBC4BD8ED284A66A0B63B50F0A63CDD3A322499A1B4688292323B6AD199BC6996A89B686EBF1F852951481C79CA975DBD6BE4DAC8FD27814583330679BEF8703E204E1A63CBE34D66E437B37C3376ABCC80DD40418912AE71323AD1EA7C488C8F86A687E75C508632F9560A49DBD4F8C340A9912210253A09931D0151DB4F54E828D66E63E254D9BC744296044B62943EBD43071C2D8A374C875A10B81D7A7803234662A534315B100E15E9ADA953CD147EB8ADA1942BABA8365FDD2475370B0943C0972700E369B385D114F843425C175FD3EC8F137D7F60F68AC6B18CB90C2336BB6E9462AB31C510B538BAF8811AC52B99C8012DC02EC09751CADB966ACD947729D6B47135A76F85D6C6F7B6D37FCFF86E47A9F2BDAFCC31BC99ACEA768796B6C66ABE21764FA20DF3FC00FB68004E402C7EFE32CD9AE53B9D94FDEBB0BE721417485E670EA948824902B8161470DA14D904843694BCD2175291349405D210FE760C96C0D67B3E428803328D37465447594C6E948730A1DDB80E294BDC7A137FC77F71C040986AA3087D73CF9404DA82E3287C13FEC4082E36B6746410389C7996EA62219F2BD049AA3FB725B68B533330FAD2E3787463C874002238ACD61110F1E90B088627358E493072430B27C3664ACB9EF1AD1F2A093D7E5D8A5DF0DA0365FF11E810A626783228149BE43A8E0F439FF49407DA9D5414ABE02C01CA764959DA2C083EB4BADD6D93D17C0ACB42BB785C6EE655F3A476E696DAC8379A6F90C3784736420F4FCD3E7D91773912C97BF0A3A97499F3A7ED84A47B874E27AE9087433EBB1A8BCFB8241A87A77E8DAC5485ACE862FF48653D35B1CF921D9E91AA704F0F51EF785DDE348539823D1110E07F604A7EA3C0EB15141B9026DC5568BBEA9F3C490909A220B1895930F05A22AB1D0BF7116164AF3C6055684DF25266748BF2BB7580F9BEB9C5A1A5B69A1E73461859496D3944D2F6EBA44E70CDEEB423BED8D3FB9FAD2D9080B99BDD9484C104E45F66242D5791C314164DE268110C576361A1E545F6A21BCF8E4D99408E3ABCD613779B449784D91250C221533078CA8B3305450D9B229459BAAB161392A2536CD775495C52CC9C4D7D424C90A2778128C8A5B5888692ED53525A7B95A0B8AE7935E53A4CF573BC016CC99ADB3303AF179B129DB135F6D715014A2AB7E5F3A1B012F70017095F69CCBA783D0D7C31847F68B4CFCF6D67D222F31A568F4C596B09ACCC31CB0A67C965424F5E7B0A6A2DAC37718154960C8250D95C9971634CAF4C37298547A5E4A98ABD213CBE1D9D1EA0C2842E6D0624D1095BBF2307A108318572CB4C125D42D4B1270F27C6601D6A7DDD536403BBE3BD8073400C63900FA24B3148CAE745AEB409B239696167599C51142A680A5CE10B2623A8B439FD395BA5F75A5D3DFE7FD5817F8DCAEF40751B6D64245E5B3BC522A2A5F3D1B79C244B4384A1371D88BBD50318423A51359F6578A70CC72CDAAC6A193BC322C4BD45840844DB2570A585B680BE757119C5FAD64409DF29512007591D57590C8F9CA5C06899AD9F082170568A0F263A7F80CF79AE2DDA59E634368F753913B121D25A2703CA21B9AF918618F5AA1438F28BC83478ED1D69090C421CCC4249CE6270DE276261DE59C90A88BE22A4EFFACC0B976BB5482E6AB66FD8EAD694311DCA777A655538AD26F56B021DA70BD79D08D365AF1FF897C14618E46AE2D447B73E72FC1A668A3210D3745E7FDD240377182B1242B6DCCA5F50AECFCDF86939A316E3C0BADCE15CF4060756DFD0A2B26F273F056B5F0066C99E994E728B44C96EF8388A431B4061E48DAB34FE16C24DE1C4D50EC6C4E3F4D58F0AC2869E4E34F154DAC7128E23B0CA6217DE0B1E3DEB4503DD08F3E8C7956F4235FB94FDA69E3437524D3B633F73453EF001DF9E988F806885FDAA0035C674512D2F5EA29820B05659B745682A6A4FBDD8582366198547C68850F1CED59E1A1684242D9B8CCBAC922683FC6A1953C15255CEFE0063BD7BF27C709765AEB1B9C8334BE834559A7995FECBDDEDD5B0447490C8A3A60B789387DCB26F0320A41DDDDC721A8305A2FD9EEF681AC184A5144D4BB59FCE3450A0D6F8AD77F6EE3558CB1AB7D1D6BD8BB6F3826A9AC02366C1F9BACDC89E5404CDEB2EB9D8987C1E97C898782A10FFF1A5AB50B5CD2B933C4D48F878B7F565DDF0667BF7CA27BBF0A2E72C4136F83D7C1BF6CE7215667ED662382613E27E317BEE409CE27609151F803FFDDC5AFD6E3A40F200FEF41FEA73578FCB3ED663651AC0D65C2305EE38780EA6F9695F0DB7D8316818E2758BD7A61FBCA1B17D46AC30296DBFC85EC30196FEA6183C9805362D6B64F55F791A61EE644C49A0E14F364A0A937644D2D628DE97CD881AF363FE8A85E2AED7DD33F65FF1AB89F7D34EAE0139C0C431DAA56002F70C800543FB88AC863C5454C286CA676AC2405342A5789824B6D794B6E2536E73021D67CF319179EEABEED8A38D461242E8A3D1D3C4D4984E9289A89CAE8F5F59AF6F59A36C1358D27068BFBD928025766DF7BC90C4185A40E38406B8F530FC7791D96EA4359C7CEA21EE09071A43ED6C7C6A6BA23BDF51C7587E04B1C7621A92DB13A2CA60B46F5A15D7EE2CD0C46F2A3EB4D8B0FF6AD702FE24416713A8538E991225F9AD1B611D1AA1E98A30F58F5004C10AAEA4E9E4D9CAA027BD573EE763364E355DDA74747A97AB9555131AA1E205231AA7EE17941211F8CEA0E4B107D3A4CAAB151A7EE53138499BA9F1F7D80A9ADAC6D7B56C5AF82B3E2631AFFBE451537081B8CC865384B30337399AB0EF87CB1C657B165DDE8C8ABBB9A9E77666A451F80EA81D1C908546B704E942108E234A30C4DCCA6C800A38AC9347EC24D4F7FCCCC3C6B01D42A3CC37E4ED276A21E3EF2C18C78A4AFF3F14DC5D19C7A2A9897986857E1309BBAEB143BAB0A0F7DC917FF3ED4D4C7A5D6DBF5BF8D3AF5716E9051A703741C4F38EAE34EDD953783FBBA91723FD870C007990ED048F9A8D2516CDA063199660C6D1878A9E77229A01135453AB07380E9A88DE91C0AE2D721209A584E77007404E7F8EAA5AB723013738FDB650F2F7AFCCB1EFFE2AEEE7940EE493F22EE40F4CEA8EE3160014401B0915EC355BF7560425181CDFBB7D2ECF8FC48FAD887099E9F96474C4E4712B4EF2B0993A9F9120844F35ECB5CE9C4F01148C5338DD4C6F2955614232197D169C520F7B98B3389150159528F554CE8E40247FCE178C046CF5AD4586E9D2EBE724499631AE2390DC118BD0F3AF611453D144E130E55F5851C52EA74F6333DA54C5F44153E3548ED2A59FCA2284599147E0E84A20ED79C804834CFC00A1FE7A4F6912CA6F770978B70BC486B3FB4E028ACA3CF8E41118288BF51E2C83FE93CB8CF73D474F8DA51284B9947DC3B6599E4B1B5F85830316189B2AFCE9CB0EAAF7B32C26A6ABF30C292A5B69D2F61097298CD9CAE2AD39E8CACEACA2F8CAA2429E26646549AC79D8750954645B256B95E2445582A62CA440553C818CDFBD69257A9859C2DE2EA2F59D2A85343CE43D6C8325D8F405C4C1A89EE0B1BFB1437BBA74DC61091A1A87DEF5C6EB0A913481C2EA2DB0CD140FD958469C209247A40CA3AC00D472754120C463DCC6C36967418D5087AE04AB4E970668130DE88AB18AD6DA21EB36DA51999BD498B88836A20A10E3AC5916650F27CE206242B458391CFBDE906220F566E20B252B8AA62F30196B500D70EC4DF1CF9F1F836EA619B3BB5CDD8CDE5423976D3463D7673EDB219BB3E179443D74DD423D77AB9968468AF290119310D84A4C43E11A01B55ECE1C10F2E69279A83B0A939E23548D7215C8C6C3ABF91FC5B774034A4A4AEDCB62CB72E772B64CAB96396FF02CE75E5151F7A4906CB5524C0E5576DF8EDD7EBE285A7BA20C7E570542892B9F2A830FCBC29F936451F7FE2278E55489160C42B3A2499467554A1C8DCE63AF967A20755824C21168CBE3AF9660E815A234AB7381C1DAA5C8F3C3A4C3FAC48EFF9C452C8E2992083FF2420C081E6BB81D464424C9C2C562C5DAA97495E67F38A805687522240FABA987F0450CAA1E46131AF0868B40BE5FA654F49F85F3EA92A899FD1F22A0BDA249E4A1120B2177A5CBEB5DC70D96FDEDC25DA718D514C611613EC9868B726D9772EB3675777B0ACB5ECA600FDE432781E2CAFB629F637AF7F9DC0225EF5207056D214869409A86B7396DE65AD258A9951DB84CBBB50820894E0282FE33B1096A81AC765C6E96A1154B16E383AF8164667E9C5B6DC6C4BB464B8BEA55F7DC6162DD5F8074B6ECE07179BEA02E36309689A3176D1BF487FDAC649D4CDFB54E0B22B01814D658D6F3BDECB12FBB8AF9E3A481FB2D4105083BECEC27703D79B04012B2ED26BF0005DE686C8EF3D5C81F0A98F9A9301D16F048DF6839318AC72B02E1A187D7FF413D170B47EFCE17F7083291C21F50000 , N'6.1.3-40302')

