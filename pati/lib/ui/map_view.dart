import 'dart:async';

import 'package:flare_flutter/flare_actor.dart';
import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:pati/core/model/pati.dart';
import 'package:pati/core/services/notification_serivce.dart';
import 'package:pati/core/services/pati_service.dart';

class MapView extends StatefulWidget {
  @override
  _MapViewState createState() => _MapViewState();
}

class _MapViewState extends State<MapView> {
  Completer<GoogleMapController> _controller = Completer();

  bool isRadio = false;
  BitmapDescriptor _markerIcon;
  int percent = 0;

  LatLng _kMapCenter = LatLng(40.875257, 29.1);

  void onDataRecived() {
    showBottomSheet(
        context: context,
        builder: (context) => BottomSheet(
              onClosing: null,
              builder: (context) => Text("oke"),
            ));
  }

  @override
  void initState() {
    super.initState();
    NotificationService.instance.callback = onDataRecived;
    PatiService.instance.getPatiService().then((val) async {
      print(val);
      var pati = Pati.fromJson(val);

      percent = pati.percent.toInt();
      _kMapCenter = LatLng(pati.latitude, pati.longitude);
      final GoogleMapController controller = await _controller.future;
      controller.animateCamera(CameraUpdate.newCameraPosition(
          CameraPosition(target: _kMapCenter, zoom: 15)));
      setState(() {
        _createMarker();
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    print(MaterialLocalizations.of(context));

    return Scaffold(
      body: GoogleMap(
          markers: _createMarker(),
          mapType: MapType.normal,
          initialCameraPosition: CameraPosition(target: _kMapCenter, zoom: 15),
          onMapCreated: (GoogleMapController controller) {
            _controller.complete(controller);
            _createMarkerImageFromAsset(context);
          }),
      floatingActionButton: _fabButton,
    );
  }

  Widget get _fabButton => FloatingActionButton.extended(
        onPressed: onPressed,
        label: Text('Mama yardiminda bulun'),
        icon: Icon(Icons.pets),
      );

  void onPressed() {
    showModalBottomSheet(
        context: context,
        backgroundColor: Colors.transparent,
        builder: (context) => BottomSheet(
              enableDrag: true,
              clipBehavior: Clip.hardEdge,
              shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(25)),
              onClosing: () {
                setState(() {});
              },
              builder: (context) {
                return Column(
                  mainAxisSize: MainAxisSize.max,
                  children: <Widget>[
                    _mamaHeader,
                    spaceHelper,
                    _sendMamaLabel,
                    _petshops
                  ],
                );
              },
            ));
  }

  Widget get spaceHelper => SizedBox(height: 20);

  Widget get _petshops => Expanded(
        child: ListView.separated(
          separatorBuilder: (context, index) => Divider(thickness: 2),
          itemBuilder: (context, index) => listCard(index),
          itemCount: 3,
        ),
      );

  Widget listCard(int index) => Card(
        child: ListTile(
          onTap: showPetshopDialog,
          leading: Text(
            "Unalan",
            style: TextStyle(fontWeight: FontWeight.w300),
            textAlign: TextAlign.center,
          ),
          title: Text("Camlica Petshop"),
          trailing: FlatButton.icon(
            icon: Icon(
              Icons.attach_money,
              color: Colors.green,
            ),
            label: Text("${index * 25}"),
            onPressed: () {},
          ),
        ),
      );

  void showPetshopDialog() {
    showDialog(
        context: context,
        builder: (context) => Dialog(
              child: Card(
                child: Wrap(
                  alignment: WrapAlignment.center,
                  runAlignment: WrapAlignment.center,
                  children: <Widget>[
                    Icon(Icons.assignment_turned_in, size: 35),
                    Text(
                        "İşleminiz tamamlandı. En kısa sürede yerine ulaştırılacak")
                  ],
                ),
              ),
            ));
  }

  Widget get _mamaHeader => Expanded(
        child: FlareActor(
          "assets/Teddy.flr",
          alignment: Alignment.center,
          fit: BoxFit.cover,
          animation: "idle",
        ),
      );

  Widget get _sendMamaLabel => Column(
        children: <Widget>[
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              Text(
                "Haydi mama gönderelim",
                style: Theme.of(context).textTheme.headline,
              ),
              Icon(
                Icons.check_circle,
                color: Colors.green,
              )
            ],
          )
        ],
      );

  Set<Marker> _createMarker() {
    return <Marker>[
      Marker(
        markerId: MarkerId("marker_1"),
        infoWindow: InfoWindow(
            title: "Unalan Mama Kabi", snippet: "Doluluk orani $percent"),
        position: _kMapCenter,
        icon: _markerIcon,
        onTap: () {},
      ),
    ].toSet();
  }

  Future<void> _createMarkerImageFromAsset(BuildContext context) async {
    if (_markerIcon == null) {
      final ImageConfiguration imageConfiguration =
          createLocalImageConfiguration(context);
      BitmapDescriptor.fromAssetImage(
              imageConfiguration, 'assets/images/container.png')
          .then(_updateBitmap);
    }
  }

  void _updateBitmap(BitmapDescriptor bitmap) {
    setState(() {
      _markerIcon = bitmap;
    });
  }
}
