import 'dart:async';

import 'package:firebase_messaging/firebase_messaging.dart';
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

  BitmapDescriptor _markerIcon;

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
    PatiService.instance.getPatiService().then((val) {
      var pati = Pati.fromJson(val);
      _kMapCenter = LatLng(pati.latitute, pati.longtitute);
      setState(() {
        _createMarker();
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    _createMarkerImageFromAsset(context);

    return Scaffold(
      body: GoogleMap(
        markers: _createMarker(),
        mapType: MapType.normal,
        initialCameraPosition: CameraPosition(target: _kMapCenter, zoom: 10),
        onMapCreated: (GoogleMapController controller) {
          _controller.complete(controller);
        },
      ),
      floatingActionButton: FloatingActionButton.extended(
        onPressed: null,
        label: Text('To the lake!'),
        icon: Icon(Icons.directions_boat),
      ),
    );
  }

  Set<Marker> _createMarker() {
    return <Marker>[
      Marker(
        markerId: MarkerId("marker_1"),
        infoWindow:
            InfoWindow(title: "Adalar Mama Kabi", snippet: "Doluluk orani %15"),
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
